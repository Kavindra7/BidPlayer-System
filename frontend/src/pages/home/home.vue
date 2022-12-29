<template>
	<section  class="main-page">
	<section class="page-section q-pa-md" >
		<div class="container-fluid">
			<div class="grid ">
		<div class="col-12 comp-grid" >
			<div class="">
	<div class=" text-2xl font-bold" >
		Report Dashboard
	</div>
	<hr class="my-3" />
			</div>
		</div>
		<div class="col-4 sm:col-6 lg:col-4 md:col-4 comp-grid" >
			<div class="">
	<record-count api-path="components_data/getcount_users" max="" v-slot="record">
		<router-link  :to="`users`">
			<div class="card " >
				<div class="font-medium text-lg mb-3">Users</div>
				<div class="flex justify-content-between mb-3">
					<div style="width:auto;">
						<Avatar icon="pi pi-th-large" size="large" class="" />
					</div>
					<div>
						<div class="font-bold text-4xl" v-if="!record.loading">{{ record.num }}</div>
						<ProgressSpinner v-else style="width:20px;height:20px" />
					</div>
				</div>
				<div class="text-500 text-sm">Total Users</div>
			</div>
		</router-link>
	</record-count>
			</div>
		</div>
		<div class="col-4 sm:col-6 lg:col-4 md:col-4 comp-grid" >
			<div class="">
	<record-count api-path="components_data/getcount_teams" max="" v-slot="record">
		<router-link  :to="`teams`">
			<div class="card " >
				<div class="font-medium text-lg mb-3">Teams</div>
				<div class="flex justify-content-between mb-3">
					<div style="width:auto;">
						<Avatar icon="pi pi-th-large" size="large" class="" />
					</div>
					<div>
						<div class="font-bold text-4xl" v-if="!record.loading">{{ record.num }}</div>
						<ProgressSpinner v-else style="width:20px;height:20px" />
					</div>
				</div>
				<div class="text-500 text-sm">Total Teams</div>
			</div>
		</router-link>
	</record-count>
			</div>
		</div>
		<div class="col-4 sm:col-6 lg:col-4 md:col-4 comp-grid" >
			<div class="">
	<record-count api-path="components_data/getcount_trophies" max="" v-slot="record">
		<router-link  :to="`trophies`">
			<div class="card " >
				<div class="font-medium text-lg mb-3">Trophies</div>
				<div class="flex justify-content-between mb-3">
					<div style="width:auto;">
						<Avatar icon="pi pi-th-large" size="large" class="" />
					</div>
					<div>
						<div class="font-bold text-4xl" v-if="!record.loading">{{ record.num }}</div>
						<ProgressSpinner v-else style="width:20px;height:20px" />
					</div>
				</div>
				<div class="text-500 text-sm">Total Trophies</div>
			</div>
		</router-link>
	</record-count>
			</div>
		</div>
		<div class="col-sm-4 col-md-4 col-12 comp-grid" >
			<div class="">
	<div :class="{ 'card': !isSubPage }" class="">
		<div class="q-pa-md">
			<div class="font-bold text-lg">Bidding Prices</div>
			<div class="text-500"></div>
			<hr />
			<div class="row q-col-gutter-sm">
				<div class="col">
	<api-data-source   api-path="components_data/linechart_biddingprices" >
	<template v-slot="req">
<div style="position:relative;text-align:center">
	<div class="text-center p-3" v-if="req.loading">
		<ProgressSpinner style="width:50px;height:50px" />
	</div>
	<vue-chart :chart-data="req.response" chart-type="line" ></vue-chart>
</div>
	</template>
	</api-data-source>
				</div>
			</div>
		</div>
	</div>
			</div>
		</div>
			</div>
		</div>
	</section>
	</section>
</template>
<script setup>
	import {  ref } from 'vue';
	import { useApp } from '@/composables/app.js';
	import VueChart from '@/components/VueCharts.vue';
	const props = defineProps({
		pageName: {
			type: String,
			default: 'home',
		},
		routeName: {
			type: String,
			default: 'home',
		},
		isSubPage: {
			type : Boolean,
			default : false,
		},
	});
	const app = useApp();
	const pageReady = ref(true);
</script>
<style scoped>
</style>
