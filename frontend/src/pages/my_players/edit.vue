<template>
	<div class="main-page">
<template v-if="showHeader">
	<section class="page-section mb-3" >
		<div class="container">
			<div class="grid justify-content-between">
		<div class="col " >
			<div class="">
	<div class=" text-2xl text-primary font-bold" >
		Edit My Players
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
<div class="col-12">
	<div class="formgrid grid">
		<div class="col-12 md:col-3">
			Status 
		</div>
		<div class="col-12 md:col-9">
<InputText ref="ctrlstatus" v-model.trim="formData.status"  label="Status" type="text" placeholder="Enter Status"      
class=" w-full" :class="getErrorClass('status')">
</InputText>
	<small v-if="isFieldValid('status')" class="p-error">{{ getFieldError('status') }}</small> 
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
</script>
<style scoped>
</style>
