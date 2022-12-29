<template>
	<div class="grid justify-content-center">
		<div class="col md:col-5">
			<Message @close="errorMsg=null" class="fadeinUp" v-if="errorMsg" severity="error" key="error">{{errorMsg}}</Message>
			<div class="card">
				<div class="my-3">
					<div class="text-2xl font-bold">Password Reset </div>
					<small class="text-500">
						Please provide the valid email address you used to register
					</small>
				</div>
				<Message severity="success" v-if="sentMsg">{{ sentMsg }}</Message>
				<form ref="observer" tag="form" @submit.prevent="sendEmail()">
					<div class="grid align-items-center justify-content-between">
						<div class="col">
							<InputText class="w-full" v-model="formData.email" placeholder="Email" required type="email" />
						</div>
						<div class="col-auto">
							<Button :loading="saving" type="submit" label="Send" icon="pi pi-envelope" />
						</div>
					</div>
				</form>
				<hr />
				<div class="text-primary">
					A link will be sent to your email containing the information you need for your password
				</div>
			</div>
		</div>
	</div>
</template>
<script>
export default {
    data: function () {
        return {
			formData: {
				email: ""
			},
			saving:false,
			errorMsg:null,
			sentMsg: null,
        };
    },
    methods: {
		async sendEmail(){
			this.saving = true;
			this.errorMsg = null;
			this.$api.post("auth/forgotpassword", this.formData).then( (response) => {
				this.saving = false;
				this.sentMsg = response.data;
			},
			(request) => {
				this.saving = false;
				this.sentMsg = null;
				this.errorMsg = request?.response?.data || "Unable to complete request";
			});
		},
	},
};
</script>