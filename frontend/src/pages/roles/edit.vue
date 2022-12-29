<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Edit Roles
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
                                                                    Role Name *
                                                                </div>
                                                                <div class="col-12 md:col-9">
                                                                    <InputText ref="ctrlrole_name" v-model.trim="formData.role_name"  label="Role Name" type="text" placeholder="Enter Role Name"      
                                                                    class=" w-full" :class="getErrorClass('role_name')">
                                                                    </InputText>
                                                                    <small v-if="isFieldValid('role_name')" class="p-error">{{ getFieldError('role_name') }}</small> 
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
	import { required } from '@/services/validators';
	import { useApp } from '@/composables/app.js';
	import { useEditPage } from '@/composables/editpage.js';
	const props = defineProps({
		id: [String, Number],
		pageName: {
			type: String,
			default: 'roles',
		},
		routeName: {
			type: String,
			default: 'rolesedit',
		},
		pagePath: {
			type : String,
			default : 'roles/edit',
		},
		apiPath: {
			type: String,
			default: 'roles/edit',
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
		role_name: "", 
	}, props.pageData);
	const formData = reactive({ ...formDefaultValues });
	function onFormSubmited(response) {
		app.flashMsg(props.msgAfterUpdate);
		if(props.redirect) app.navigateTo(`/roles`);
	}
	const rules = computed(() => {
		return {
			role_name: { required }
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
		const pageTitle = "Edit Roles";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
