<template>
	<div class="main-page">
<template v-if="showHeader">
	<section class="page-section mb-3" >
		<div class="container">
			<div class="grid justify-content-between">
		<div class="col " >
			<div class="">
	<div class=" text-2xl text-primary font-bold" >
		Add New Player
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
			Player Name 
		</div>
		<div class="col-12 md:col-9">
	<api-data-source   api-path="components_data/player_name_option_list" >
	<template v-slot="req">
	<Dropdown class="w-full" :class="getErrorClass('player_name')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlplayer_name"  v-model="formData.player_name" :options="req.response" label="Player Name"  placeholder="Select a value ..." >
	</Dropdown>	
	<small v-if="isFieldValid('player_name')" class="p-error">{{ getFieldError('player_name') }}</small> 
	</template>
	</api-data-source>
		</div>
	</div>
</div>
<div class="col-12">
	<div class="formgrid grid">
		<div class="col-12 md:col-3">
			Bid Price 
		</div>
		<div class="col-12 md:col-9">
<InputText ref="ctrlbid_price" v-model.trim="formData.bid_price"  label="Bid Price" type="number" placeholder="Enter Bid Price"   step="0.1"    
class=" w-full" :class="getErrorClass('bid_price')">
</InputText>
	<small v-if="isFieldValid('bid_price')" class="p-error">{{ getFieldError('bid_price') }}</small> 
		</div>
	</div>
</div>
<div class="col-12 hidden">
	<div class="formgrid grid">
		<div class="col-12 md:col-3">
			Status 
		</div>
		<div class="col-12 md:col-9 ">
<InputText ref="ctrlstatus" v-model.trim="formData.status" value="Pending"  label="Status" type="text" placeholder="Enter Status"      
class=" w-full" :class="getErrorClass('status')">
</InputText>
	<small v-if="isFieldValid('status')" class="p-error">{{ getFieldError('status') }}</small> 
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
			default : 'my_players',
		},
		routeName : {
			type : String,
			default : 'my_playersadd',
		},
		apiPath : {
			type : String,
			default : 'my_players/add',
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
		player_name: "", 
		bid_price: "NULL", 
		status: "NULL", 
	};
	const formData = reactive({ ...formDefaultValues });
	//form validation rules
	const rules = computed(() => {
		return {
			player_name: {  },
			bid_price: { numeric },
			status: {  }
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
		if(props.redirect) app.navigateTo(`/my_players`);
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
		const pageTitle = "Add New My Player";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
	});
	// expose page object for other components access
	defineExpose({
		page
	});
</script>
<style scoped>
</style>
