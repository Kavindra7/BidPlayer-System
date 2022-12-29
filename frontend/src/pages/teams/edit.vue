<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Edit Teams
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </template>
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
                                                                    Team Name 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlteam_name" v-model.trim="formData.team_name"  label="Team Name" type="text" placeholder="Enter Team Name"      
                                                                    class=" w-full" :class="getErrorClass('team_name')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('team_name')" class="p-error">{{ getFieldError('team_name') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Max Players 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlmax_players" v-model.trim="formData.max_players"  label="Max Players" type="text" placeholder="Enter Max Players"      
                                                                    class=" w-full" :class="getErrorClass('max_players')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('max_players')" class="p-error">{{ getFieldError('max_players') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Max Price 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlmax_price" v-model.trim="formData.max_price"  label="Max Price" type="number" placeholder="Enter Max Price"   step="0.1"    
                                                                    class=" w-full" :class="getErrorClass('max_price')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('max_price')" class="p-error">{{ getFieldError('max_price') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Status 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <div class="field-radiobutton" v-for="option of app.menus.statusItems" :key="option.value">
                                                                        <RadioButton :class="getErrorClass('status')" :id="option.value" name="ctrlstatus" :value="option.value" v-model="formData.status" />
                                                                        <label :for="option.value">{{option.label}} </label>
                                                                    </div>
                                                                    <small v-if="isFieldValid('status')" class="p-error">{{ getFieldError('status') }}</small> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="formgrid grid">
                                                                <div class="col-12 md:col-3">
                                                                    Owner 
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <api-data-source   api-path="components_data/owner_id_option_list" >
                                                                        <template v-slot="req">
                                                                            <Dropdown class="w-full" :class="getErrorClass('owner_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlowner_id"  v-model="formData.owner_id" :options="req.response" label="Owner"  placeholder="Select a value ..." >
                                                                            </Dropdown> 
                                                                            <small v-if="isFieldValid('owner_id')" class="p-error">{{ getFieldError('owner_id') }}</small> 
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
			default: 'teams',
		},
		routeName: {
			type: String,
			default: 'teamsedit',
		},
		pagePath: {
			type : String,
			default : 'teams/edit',
		},
		apiPath: {
			type: String,
			default: 'teams/edit',
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
		team_name: "NULL", 
		max_players: "", 
		max_price: "NULL", 
		status: "", 
		owner_id: "", 
	}, props.pageData);
	const formData = reactive({ ...formDefaultValues });
	function onFormSubmited(response) {
		app.flashMsg(props.msgAfterUpdate);
		if(props.redirect) app.navigateTo(`/teams`);
	}
	const rules = computed(() => {
		return {
			team_name: {  },
			max_players: {  },
			max_price: { numeric },
			status: {  },
			owner_id: { numeric }
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
		const pageTitle = "Edit Teams";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
