<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Teams Details
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
                                        <template v-if="pageReady">
                                            <div class="grid">
                                                <div class="col-12">
                                                    <div class="" :class="{ 'card': !isSubPage }">
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Id: 
                                                            </div>
                                                            <div class="col font-bold">{{ item.id }}</div>
                                                        </div>
                                                        <hr />
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Team Name: 
                                                            </div>
                                                            <div class="col font-bold">{{ item.team_name }}</div>
                                                        </div>
                                                        <hr />
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Max Players: 
                                                            </div>
                                                            <div class="col font-bold">{{ item.max_players }}</div>
                                                        </div>
                                                        <hr />
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Max Price: 
                                                            </div>
                                                            <div class="col font-bold">{{ item.max_price }}</div>
                                                        </div>
                                                        <hr />
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Status: 
                                                            </div>
                                                            <div class="col font-bold">{{ item.status }}</div>
                                                        </div>
                                                        <hr />
                                                        <div class="grid align-items-center">
                                                            <div class="col">
                                                                Owner Name: 
                                                            </div>
                                                            <div class="col font-bold">
                                                                <Button class="p-button-text" icon="pi pi-eye" :label="item.users_full_name" v-if="item.owner_id" @click="app.openPageDialog({ page: 'users/view', url: `/users/view/${item.owner_id}` , closeBtn: true })" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="flex  justify-btween">
                                                            <SplitButton class="p-button p-button-raised p-button-text p-button-sm" :model="getActionMenuModel(item)">
                                                                <i></i>
                                                            </SplitButton>
                                                        </div>
                                                    </div>
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
            </div>
        </section>
    </div>
</template>
<script setup>
	import {   toRefs, onMounted } from 'vue';
	import { useApp } from '@/composables/app.js';
	import { useAuth } from '@/composables/auth';
	import { useViewPage } from '@/composables/viewpage.js';
	const props = defineProps({
		id: [String, Number],
		primaryKey: {
			type: String,
			default: 'id',
		},
		pageName: {
			type: String,
			default: 'teams',
		},
		routeName: {
			type: String,
			default: 'teamsview',
		},
		apiPath: {
			type: String,
			default: 'teams/view',
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
		}
	});
	const app = useApp(props);
	const auth = useAuth();
	const page = useViewPage(props); // page hook
	//page state
	const { 
		item, // current record from store - Object
		loading, // api loading state
		pageReady, // api data loaded successfully
	} = toRefs(page.state);
	//page computed propties
	const {
		apiUrl, // page current api path
		currentRecord, // current page record  - Object
	} = page.computedProps
	//page methods
	const 
	{ 
		load, // load data from api
		deleteItem, // deleted current record
		exportPage, // export page records - args = (exportFormats, apiUrl, pageName)
		moveToNextRecord, // load next record from api
		moveToPreviousRecord // load previous record from api
	} = page.methods
	function getActionMenuModel(data){
		return [
		{
			label: "Edit",
			command: (event) => { app.openPageDialog({ page:'teams/edit', url: `/teams/edit/${data.id}` , closeBtn: true }) },
			icon: "pi pi-pencil",
			visible: auth.canView('teams/edit')
		},
		{
			label: "Delete",
			command: (event) => { deleteItem(data.id) },
			icon: "pi pi-minus-circle",
			visible: auth.canView('teams/delete')
		}
	]
	}
	onMounted(()=>{ 
		const pageTitle = "Teams Details";
		app.setPageTitle(props.routeName, pageTitle); // set browser page title
		load();
	});
</script>
<style scoped>
</style>
