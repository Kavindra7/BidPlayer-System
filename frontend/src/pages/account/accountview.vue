<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    My Account
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
                    <div class="col comp-grid" >
                        <div class="">
                            <div class="">
                                <div >
                                    <div class="relative-position" style="min-height:100px">
                                        <div class="grid">
                                            <div class="col-12">
                                                <template v-if="pageReady">
                                                    <div class="" :class="{ 'card': !isSubPage }">
                                                        <div class="p-4 mb-4">
                                                            <div class="grid align-items-center">
                                                                <div class="col-fixed" style="width:120x">
                                                                    <Avatar icon="pi pi-user" size="xlarge" shape="circle" />
                                                                    </div>
                                                                    <div class="col">
                                                                        <div class="text-2xl text-primary"> {{ $auth.userName }} </div>
                                                                        <div class="text-sm text-gray-500"> {{ $auth.userEmail }} </div>
                                                                        <div class="text-sm text-500 text-capitalize py-2" v-if="$auth.userRole">{{ $auth.userRole}}</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <TabView v-model:activeIndex="activeTab">
                                                                <TabPanel>
                                                                    <template #header>
                                                                        <i class="pi pi-user mr-2"></i>
                                                                        <span>Account Detail</span>
                                                                    </template>
                                                                    <div>
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Id: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.id }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Full Name: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.full_name }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Username: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.username }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Email: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.email }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Age: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.age }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Gender: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.gender }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                Balance: 
                                                                            </div>
                                                                            <div class="col font-bold">{{ item.balance }}</div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="grid align-items-center">
                                                                            <div class="col">
                                                                                User Role: 
                                                                            </div>
                                                                            <div class="col font-bold">
                                                                                <Button class="p-button-text" icon="pi pi-eye" label="Roles Detail" v-if="item.user_role_id" @click="app.openPageDialog({ page: 'roles/view', url: `/roles/view/${item.user_role_id}` , closeBtn: true })" />
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                    </div>
                                                                </TabPanel>
                                                                <TabPanel>
                                                                    <template #header>
                                                                        <i class="pi pi-pencil  mr-2"></i>
                                                                        <span>Edit Account</span>
                                                                    </template>
                                                                    <div class="reset-grid">
                                                                        <account-edit-page></account-edit-page>
                                                                        </div>
                                                                    </TabPanel>
                                                                    <TabPanel>
                                                                        <template #header>
                                                                            <i class="pi pi-lock  mr-2"></i>
                                                                            <span>Change Password</span>
                                                                        </template>
                                                                        <div class="reset-grid">
                                                                            <change-password-page></change-password-page>
                                                                        </div>
                                                                    </TabPanel>
                                                                </TabView>
                                                            </div>
                                                        </template>
                                                        <template v-if="loading">
                                                            <div  class="q-pa-lg text-center">
                                                                <ProgressSpinner style="width:50px;height:50px" /> Loading...
                                                            </div>
                                                        </template>
                                                    </div>
                                                </div>
                                            </div>
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
	import {  ref, toRefs, onMounted } from 'vue';
	import { useApp } from '@/composables/app.js';
	import { useAuth } from '@/composables/auth';
	import  AccountEditPage  from "./accountedit.vue";
	import  ChangePasswordPage  from "./changepassword.vue";
	import { useViewPage } from '@/composables/viewpage.js';
	const props = defineProps({
		id: [String, Number],
		primaryKey: {
			type: String,
			default: 'id',
		},
		pageName: {
			type: String,
			default: 'users',
		},
		routeName: {
			type: String,
			default: 'usersaccountview',
		},
		apiPath: {
			type: String,
			default: 'account',
		},
		editButton: {
			type: Boolean,
			default: true,
		},
		deleteButton: {
			type: Boolean,
			default: true,
		},
		exportButton: {
			type: Boolean,
			default: true,
		},
		titleBeforeDelete: {
			type: String,
			default: "Delete record",
		},
		msgBeforeDelete: {
			type: String,
			default: "Record deleted successfully",
		},
		msgAfterDelete: {
			type: String,
			default: "Record deleted successfully",
		},
		showHeader: {
			type: Boolean,
			default: true,
		},
		showFooter: {
			type: Boolean,
			default: true,
		},
		isSubPage: {
			type : Boolean,
			default : false,
		},
	});
	const app = useApp(props);
	const auth = useAuth();
	const page = useViewPage(props);
	const activeTab = ref(0)
	//page state
	const { 
		item, 
		loading, 
		pageReady, // if api data loaded successfully
	} = toRefs(page.state);
	//page computed propties
	const {
		apiUrl, // page current api path
		currentRecord, // current page record  - Object
	} = page.computedProps
	//page methods
	const { load,  deleteItem } = page.methods
	onMounted(()=>{
		const pageTitle = "My Account";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
