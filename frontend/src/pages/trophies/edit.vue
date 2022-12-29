<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Edit Trophies
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
			default: 'trophies',
		},
		routeName: {
			type: String,
			default: 'trophiesedit',
		},
		pagePath: {
			type : String,
			default : 'trophies/edit',
		},
		apiPath: {
			type: String,
			default: 'trophies/edit',
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
		trophy_name: "NULL", 
		team_1: "", 
		team_2: "", 
		prize: "NULL", 
	}, props.pageData);
	const formData = reactive({ ...formDefaultValues });
	function onFormSubmited(response) {
		app.flashMsg(props.msgAfterUpdate);
		if(props.redirect) app.navigateTo(`/trophies`);
	}
	const rules = computed(() => {
		return {
			trophy_name: {  },
			team_1: { numeric },
			team_2: { numeric },
			prize: { numeric }
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
		const pageTitle = "Edit Trophies";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
