<template>
	<div class="main-page">
<template v-if="showHeader">
	<section class="page-section mb-3" >
		<div class="container">
			<div class="grid justify-content-between">
		<div class="col " >
			<div class="">
	<div class=" text-2xl text-primary font-bold" >
		Bid New Player
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
<div v-if="$auth.userRole != 'Team Owner'" class="col-12">
	<div class="formgrid grid">
		<div class="col-12 md:col-3">
			Player 
		</div>
		<div class="col-12 md:col-9">
	<api-data-source   api-path="components_data/player_id_option_list" >
	<template v-slot="req">
	<Dropdown class="w-full" :class="getErrorClass('player_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlplayer_id"  v-model="formData.player_id" :options="req.response" label="Player"  placeholder="Select a value ..." >
	</Dropdown>	
	<small v-if="isFieldValid('player_id')" class="p-error">{{ getFieldError('player_id') }}</small> 
	</template>
	</api-data-source>
		</div>
	</div>
</div>
<div v-if="$auth.userRole != 'Team Owner'" class="col-12">
	<div class="formgrid grid">
		<div class="col-12 md:col-3">
			Thophy 
		</div>
		<div class="col-12 md:col-9">
	<api-data-source   api-path="components_data/thophy_id_option_list" >
	<template v-slot="req">
	<Dropdown class="w-full" :class="getErrorClass('thophy_id')"   :loading="req.loading"   optionLabel="label" optionValue="value" ref="ctrlthophy_id"  v-model="formData.thophy_id" :options="req.response" label="Thophy"  placeholder="Select a value ..." >
	</Dropdown>	
	<small v-if="isFieldValid('thophy_id')" class="p-error">{{ getFieldError('thophy_id') }}</small> 
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
		<div class="col-12 md:col-9">
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
			default : 'bid_players',
		},
		routeName : {
			type : String,
			default : 'bid_playersadd',
		},
		apiPath : {
			type : String,
			default : 'bid_players/add',
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
		player_id: localStorage.getItem('bid_playerID'), 
		thophy_id: localStorage.getItem('bid_trophyID'), 
		bid_price: "10000.00", 
		status: "Pending", 
	};
	const formData = reactive({ ...formDefaultValues });
	//form validation rules
	const rules = computed(() => {
		return {
			player_id: { numeric },
			thophy_id: { numeric },
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
		if(props.redirect) app.navigateTo(`/bid_players`);
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
		const pageTitle = "Bid a Player";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
	});
	// expose page object for other components access
	defineExpose({
		page
	});
</script>
<style scoped>
</style>
