<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Add New Users
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
                                    <form ref="observer" tag="form" @submit.prevent="submitForm()" class="" :class="{ 'card': !isSubPage }">
                                        <div class="grid">
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Username *
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <check-duplicate v-model="formData.username" check-path="components_data/users_username_exist/" v-slot="checker">
                                                        <InputText ref="ctrlusername" @blur="checker.check" :loading="checker.loading" v-model.trim="formData.username"  label="Username" type="text" placeholder="Enter Username"      
                                                        class=" w-full" :class="getErrorClass('username')">
                                                        </InputText>
                                                        <small v-if="isFieldValid('username')" class="p-error">{{ getFieldError('username') }}</small> 
                                                        <small v-if="!checker.loading && checker.exist" class="p-error"> Not available</small>
                                                        <small v-if="checker.loading" class="text-500">Checking...</small>
                                                        </check-duplicate>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Email *
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <check-duplicate v-model="formData.email" check-path="components_data/users_email_exist/" v-slot="checker">
                                                        <InputText ref="ctrlemail" @blur="checker.check" :loading="checker.loading" v-model.trim="formData.email"  label="Email" type="email" placeholder="Enter Email"      
                                                        class=" w-full" :class="getErrorClass('email')">
                                                        </InputText>
                                                        <small v-if="isFieldValid('email')" class="p-error">{{ getFieldError('email') }}</small> 
                                                        <small v-if="!checker.loading && checker.exist" class="p-error"> Not available</small>
                                                        <small v-if="checker.loading" class="text-500">Checking...</small>
                                                        </check-duplicate>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Password *
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <Password ref="ctrlpassword" v-model="formData.password" label="Password" placeholder="Enter Password"   
                                                        class="w-full" inputClass="w-full" toggleMask :feedback="true" :class="getErrorClass('password')" />
                                                        <small v-if="isFieldValid('password')" class="p-error">{{ getFieldError('password') }}</small> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Confirm Password *
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <Password  class="w-full" :class="getErrorClass('password')" inputClass="w-full" :feedback="false" toggleMask ref="ctrlconfirmpassword" v-model="formData.confirm_password" label="Confirm Password" placeholder="Confirm Password"  />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        User Role Id 
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <api-data-source   api-path="components_data/role_id_option_list" >
                                                            <template v-slot="req">
                                                                <Dropdown class="w-full" :class="getErrorClass('user_role_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrluser_role_id"  v-model="formData.user_role_id" :options="req.response" label="User Role Id"  placeholder="Select a value ..." >
                                                                </Dropdown> 
                                                                <small v-if="isFieldValid('user_role_id')" class="p-error">{{ getFieldError('user_role_id') }}</small> 
                                                            </template>
                                                        </api-data-source>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div v-if="showSubmitButton" class="text-center my-3">
                                            <Button class="p-button-primary" type="submit" label="Submit" icon="pi pi-send" :loading="saving" />
                                        </div>
                                    </form>
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
	import { required, email, sameAs, numeric, } from '@/services/validators';
	import { useApp } from '@/composables/app.js';
	import { useAddPage } from '@/composables/addpage.js';
	const props = defineProps({
		pageName : {
			type : String,
			default : 'users',
		},
		routeName : {
			type : String,
			default : 'usersadd',
		},
		apiPath : {
			type : String,
			default : 'users/add',
		},
		submitButtonLabel: {
			type: String,
			default: "Submit",
		},
		formValidationError: {
			type: String,
			default: "Form is invalid",
		},
		formValidationMsg: {
			type: String,
			default: "Please complete the form",
		},
		msgAfterSave: {
			type: String,
			default: "Record added successfully",
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
		pageData: { // use to set formData default values from another page
			type: Object,
			default: () => {} 
		},
	});
	const app = useApp();
	const formDefaultValues = {
		username: "", 
		email: "", 
		password: "", 
		confirm_password: "", 
		user_role_id: "", 
	};
	const formData = reactive({ ...formDefaultValues });
	//form validation rules
	const rules = computed(() => {
		return {
			username: { required },
			email: { required, email },
			password: { required },
			confirm_password: {required, sameAs: sameAs(formData.password, 'Password') },
			user_role_id: { numeric }
		}
	});
	const v$ = useVuelidate(rules, formData); // form validation
	const page = useAddPage({ props, formData, v$, beforeSubmit, afterSubmit });
	// perform custom validation before submit form
	// set custom form fields
	// return false if validation fails
	function beforeSubmit(){
		return true;
	}
	// after form submited to api
	// reset form data.
	// redirect to another page
	function afterSubmit(response) {
		app.flashMsg(props.msgAfterSave);
		page.setFormDefaultValues(); //reset form data
		if(props.redirect) app.navigateTo(`/users`);
	}
	//page state
	const {
		submitted, // form submitted state - Boolean
		saving, // form api submiting state - Boolean
		pageReady, // if api data loaded successfully
	} = toRefs(page.state);
	//page methods
	const {
		submitForm, //submit form data to api
		getErrorClass, // return error class if field is invalid- args(fieldname)
		getFieldError, //  get validation error message - args(fieldname)
		isFieldValid, // check if field is validated - args(fieldname)
		 // map api datasource  to Select options label-value
	} = page.methods;
	onMounted(()=>{
		const pageTitle = "Add New Users";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
	});
	// expose page object for other components access
	defineExpose({
		page
	});
</script>
<style scoped>
</style>
