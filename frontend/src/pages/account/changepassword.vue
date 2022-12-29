
	
<template>
	<div class="grid">
		<div class="col-12 md:col-5">
		<Message @close="errorMsg=null" class="fadeinUp" v-if="errorMsg" severity="error" key="error">{{errorMsg}}</Message>
			<div class="text-2xl font-bold my-4">Change Password</div>
			<form ref="observer"  tag="form" @submit.prevent="submitForm()">
				<div class="field">
					<Password class="w-full" :class="getErrorClass('oldpassword')"  inputClass="w-full" :feedback="false" :toggleMask="true" label="Current Password"  placeholder="Current Password"  v-model="formData.oldpassword"  />
				</div>
				<div class="field">
					<Password class="w-full" :class="getErrorClass('newpassword')" inputClass="w-full" :toggleMask="true" label="New Password"  placeholder="New Password"  v-model="formData.newpassword" 	/>
				</div>
				<div class="field">
					<Password class="w-full" :class="getErrorClass('confirmpassword')" inputClass="w-full" :feedback="false" :toggleMask="true" label="Confirm new password"  placeholder="Confirm new password" v-model="formData.confirmpassword"  />
				</div>
				<div class="my-3 text-center">
					<Button type="submit" icon="pi pi-send" label="Change Password" :loading="saving" />
				</div>
			</form>
		</div>
	</div>
</template>
<script>
import useVuelidate from '@vuelidate/core';
import { required, sameAs } from '@vuelidate/validators';
import { useApp } from '@/composables/app';
export default {
	setup () {
		const app = useApp();
		return {app, v$: useVuelidate() }
	},
	props:{
		apiUrl: {
			type: String,
			default: 'account/changepassword'
		},
	},
	data() {
		return {
			errorMsg: null,
			saving: false,
			submitted: false,
			formData: {
				oldpassword: '', 
				newpassword: '', 
				confirmpassword: '', 
			},
		}
	},
	validations() {
			let formData =  {
				oldpassword: { required },
				newpassword: { required },
				confirmpassword: { 
					required, 
					sameAsPassword: sameAs(this.formData.newpassword),
				}
			}
			return {formData}
		},
	methods: {
		async submitForm(){
			this.submitted = true;
			const isFormValid = !this.v$.$invalid;
			if(!isFormValid){
				this.app.flashMsg("Form is invalid", "Please complete the form", "error");
				return;
			}
			this.saving = true;
			let payload = this.formData;
			let url = this.apiUrl;
			this.$api.post(url, payload).then((response) => {
				this.saving = false;
				this.submitted = false;
				this.resetForm();
				this.app.flashMsg("Password change completed");
			},(request) => {
				this.saving = false;
				this.errorMsg = request?.response?.data || "Unable to complete request";
			});
		},
		getErrorClass(field){
			let isValid = this.v$.formData[field].$invalid && this.submitted
			return {"p-invalid": isValid};
		},
		resetForm (){
			this.formData = {oldpassword: "", newpassword: "", confirmpassword: "", };
			this.$refs.observer.reset();
		},
	},
}
</script>
