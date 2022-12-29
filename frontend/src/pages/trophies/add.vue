<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Add New Trophies
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
                                                        Trophy Name 
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <InputText ref="ctrltrophy_name" v-model.trim="formData.trophy_name"  label="Trophy Name" type="text" placeholder="Enter Trophy Name"      
                                                        class=" w-full" :class="getErrorClass('trophy_name')">
                                                        </InputText>
                                                        <small v-if="isFieldValid('trophy_name')" class="p-error">{{ getFieldError('trophy_name') }}</small> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Team 1 
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <api-data-source   api-path="components_data/team_1_option_list" >
                                                            <template v-slot="req">
                                                                <Dropdown class="w-full" :class="getErrorClass('team_1')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlteam_1"  v-model="formData.team_1" :options="req.response" label="Team 1"  placeholder="Select a value ..." >
                                                                </Dropdown> 
                                                                <small v-if="isFieldValid('team_1')" class="p-error">{{ getFieldError('team_1') }}</small> 
                                                            </template>
                                                        </api-data-source>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Team 2 
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <api-data-source   api-path="components_data/team_1_option_list" >
                                                            <template v-slot="req">
                                                                <Dropdown class="w-full" :class="getErrorClass('team_2')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlteam_2"  v-model="formData.team_2" :options="req.response" label="Team 2"  placeholder="Select a value ..." >
                                                                </Dropdown> 
                                                                <small v-if="isFieldValid('team_2')" class="p-error">{{ getFieldError('team_2') }}</small> 
                                                            </template>
                                                        </api-data-source>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="formgrid grid">
                                                    <div class="col-12 md:col-3">
                                                        Prize 
                                                    </div>
                                                    <div class="col-12 md:col-9">
                                                        <InputText ref="ctrlprize" v-model.trim="formData.prize"  label="Prize" type="number" placeholder="Enter Prize"   step="0.1"    
                                                        class=" w-full" :class="getErrorClass('prize')">
                                                        </InputText>
                                                        <small v-if="isFieldValid('prize')" class="p-error">{{ getFieldError('prize') }}</small> 
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
			default : 'trophies',
		},
		routeName : {
			type : String,
			default : 'trophiesadd',
		},
		apiPath : {
			type : String,
			default : 'trophies/add',
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
		trophy_name: "NULL", 
		team_1: "", 
		team_2: "", 
		prize: "NULL", 
	};
	const formData = reactive({ ...formDefaultValues });
	//form validation rules
	const rules = computed(() => {
		return {
			trophy_name: {  },
			team_1: { numeric },
			team_2: { numeric },
			prize: { numeric }
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
		if(props.redirect) app.navigateTo(`/trophies`);
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
		const pageTitle = "Add New Trophy";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
	});
	// expose page object for other components access
	defineExpose({
		page
	});
</script>
<style scoped>
</style>
