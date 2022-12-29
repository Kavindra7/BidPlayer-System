<template>
    <div class="main-page">
        <section class="page-section " >
            <div class="container">
                <div class="grid ">
                    <div class="md:col-9 sm:col-12 comp-grid" >
                        <div class="">
                            <div class="">
                                <div >
                                    <template v-if="pageReady">
                                        <div class="grid">
                                            <div class="col-12">
                                                <form ref="observer"  tag="form" @submit.prevent="submitForm()" class="" :class="{ 'card': !isSubPage }">
                                                    <!--[form-content-start]-->
                                                    <div class="grid">
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Full Name 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlfull_name" v-model.trim="formData.full_name"  label="Full Name" type="text" placeholder="Enter Full Name"      
                                                                    class=" w-full" :class="getErrorClass('full_name')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('full_name')" class="p-error">{{ getFieldError('full_name') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Username *
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlusername" v-model.trim="formData.username"  label="Username" type="text" placeholder="Enter Username"      
                                                                    class=" w-full" :class="getErrorClass('username')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('username')" class="p-error">{{ getFieldError('username') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Age 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlage" v-model.trim="formData.age"  label="Age" type="number" placeholder="Enter Age"   step="any"    
                                                                    class=" w-full" :class="getErrorClass('age')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('age')" class="p-error">{{ getFieldError('age') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Gender 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <div class="field-radiobutton" v-for="option of app.menus.genderItems" :key="option.value">
                                                                        <RadioButton :class="getErrorClass('gender')" :id="option.value" name="ctrlgender" :value="option.value" v-model="formData.gender" />
                                                                        <label :for="option.value">{{option.label}} </label>
                                                                    </div>
                                                                    <small v-if="isFieldValid('gender')" class="p-error">{{ getFieldError('gender') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Balance 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlbalance" v-model.trim="formData.balance"  label="Balance" type="number" placeholder="Enter Balance"   step="0.1"  readonly  
                                                                    class=" w-full" :class="getErrorClass('balance')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('balance')" class="p-error">{{ getFieldError('balance') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 hidden">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    User Role 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <api-data-source   api-path="components_data/role_id_option_list" >
                                                                        <template v-slot="req">
                                                                            <Dropdown class="w-full" :class="getErrorClass('user_role_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrluser_role_id"  v-model="formData.user_role_id" :options="req.response" label="User Role"  placeholder="Select a value ..." >
                                                                            </Dropdown> 
                                                                            <small v-if="isFieldValid('user_role_id')" class="p-error">{{ getFieldError('user_role_id') }}</small> 
                                                                        </template>
                                                                    </api-data-source>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--[form-content-end]-->
                                                    <div v-if="showSubmitButton" class="text-center my-3">
                                                        <Button type="submit" label="Update" icon="pi pi-send" :loading="saving" />
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                    </template>
                                    <template v-if="loading">
                                        <div class="p-3 text-center">
                                            <ProgressSpinner style="width:50px;height:50px" />
                                        </div>
                                    </template>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>
<script setup>
	import {  computed,  reactive, toRefs, onMounted } from 'vue';
	import useVuelidate from '@vuelidate/core';
	import { required, numeric, } from '@/services/validators';
	import { useApp } from '@/composables/app.js';
	import { useEditPage } from '@/composables/editpage.js';
	const props = defineProps({
		id: [String, Number],
		pageName: {
			type: String,
			default: 'users',
		},
		routeName: {
			type: String,
			default: 'usersaccountedit',
		},
		pagePath: {
			type : String,
			default : 'account/edit',
		},
		apiPath: {
			type: String,
			default: 'account/edit',
		},
		submitButtonLabel: {
			type: String,
			default: "Update",
		},
		formValidationError: {
			type: String,
			default: "Form is invalid",
		},
		formValidationMsg: {
			type: String,
			default: "Please complete the form",
		},
		msgAfterUpdate: {
			type: String,
			default: "Record updated successfully",
		},
		showHeader: {
			type: Boolean,
			default: true,
		},
		showSubmitButton: {
			type: Boolean,
			default: true,
		},
		redirect: {
			type : Boolean,
			default : true,
		},
		isSubPage: {
			type : Boolean,
			default : false,
		},
	});
	const app = useApp();
	const formDefaultValues = Object.assign({
		full_name: "NULL", 
		username: "", 
		age: "", 
		gender: "", 
		balance: "NULL", 
		user_role_id: "", 
	}, props.pageData);
	const formData = reactive({ ...formDefaultValues });
	function onFormSubmited(response) {
		app.flashMsg(props.msgAfterUpdate);
		location.reload();
	}
	const rules = computed(() => {
		return {
			full_name: {  },
			username: { required },
			age: { numeric },
			gender: {  },
			balance: { numeric },
			user_role_id: { numeric }
		}
	});
	const v$ = useVuelidate(rules, formData); //form validation
	const page = useEditPage(props, formData, v$, onFormSubmited);
	//page state
	const {
		submitted, // form api submitted state - Boolean
		saving, // form api submiting state - Boolean
		loading, // form data loading state - Boolean
		pageReady, // if api data loaded successfully
	} = toRefs(page.state);
	//page computed propties
	const {
		apiUrl, // page current api path
		currentRecord, // current page record  - Object
	} = page.computedProps
	//page methods
	const { 
		load, // get editable form data from api
		submitForm, // submit form data to api
		getErrorClass, // return error class if field is invalid- args(fieldname)
		getFieldError, //  get validation error message - args(fieldname)
		isFieldValid, // check if field is validated - args(fieldname)
		 // map api datasource  to Select options label-value
	} = page.methods;
	onMounted(()=>{
		const pageTitle = "My Account";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
