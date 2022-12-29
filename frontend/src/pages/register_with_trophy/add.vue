<template>
	<div class="main-page">
<template v-if="showHeader">
	<section class="page-section mb-3" >
		<div class="container">
			<div class="grid justify-content-between">
		<div class="col " >
			<div class="">
	<div class=" text-2xl text-primary font-bold" >
		Add New Register With Trophy
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
			Trophy *
		</div>
		<div class="col-12 md:col-9">
	<api-data-source   api-path="components_data/thophy_id_option_list" >
	<template v-slot="req">
	<Dropdown class="w-full" :class="getErrorClass('trophy_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrltrophy_id"  v-model="formData.trophy_id" :options="req.response" label="Trophy"  placeholder="Select a value ..." >
	</Dropdown>	
	<small v-if="isFieldValid('trophy_id')" class="p-error">{{ getFieldError('trophy_id') }}</small> 
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
	import { required, numeric, } from '@/services/validators';
	import { useApp } from '@/composables/app.js';
	import { useAddPage } from '@/composables/addpage.js';
	const props = defineProps({
		pageName : {
			type : String,
			default : 'register_with_trophy',
		},
		routeName : {
			type : String,
			default : 'register_with_trophyadd',
		},
		apiPath : {
			type : String,
			default : 'register_with_trophy/add',
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
		trophy_id: "", 
	};
	const formData = reactive({ ...formDefaultValues });
	//form validation rules
	const rules = computed(() => {
		return {
			trophy_id: { required, numeric }
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
		if(props.redirect) app.navigateTo(`/register_with_trophy`);
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
		const pageTitle = "Register With Trophy";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
	});
	// expose page object for other components access
	defineExpose({
		page
	});
</script>
<style scoped>
</style>
