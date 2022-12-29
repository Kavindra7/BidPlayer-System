<template>
    <div class="main-page">
        <template v-if="showHeader">
            <section class="page-section mb-3" >
                <div class="container-fluid">
                    <div class="grid justify-content-between">
                        <div class="col " >
                            <div class="">
                                <div class=" text-2xl text-primary font-bold" >
                                    Roles
                                </div>
                            </div>
                        </div>
                        <div class="col-12 md:col-3 lg:col-2 " >
                            <div class="">
                                <template v-if="$auth.canView('/roles/add')">
                                    <Button label="Add New Roles" icon="pi pi-plus"  @click="app.openPageDialog({ page: 'roles/add', url: `/roles/add` , closeBtn: true })"  class="w-full bg-primary "  />
                                </template>
                            </div>
                        </div>
                        <div class="col-12 md:col-5 lg:col-4 " >
                            <div class="">
                                <span class="p-input-icon-left w-full">
                                <i class="pi pi-search" />
                                <InputText outlined dense  placeholder="Search" class="w-full" :value="searchText" @input="debounce(() => { searchText = $event.target.value })"  />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </template>
        <section class="page-section " >
            <div class="container-fluid">
                <div class="grid ">
                    <div class="col comp-grid" >
                        <div class="">
                            <div class="flex align-items-center justify-content-around">
                                <div v-if="searchText" :class="filterTagClass">
                                    Search
                                    <Chip class="font-medium px-2 py-1" removable @remove="clearSearch()">{{ searchText }}</Chip>
                                </div>
                            </div>
                            <div class="">
                                <div >
                                    <template v-if="showBreadcrumbs && $route.query.tag">
                                        <Breadcrumb :home="{icon: 'pi pi-home', to: '/roles'}" :model="pageBreadCrumb" />
                                    </template>
                                    <div class="grid justify-content-start">
                                        <!-- Master Page Start -->
                                        <div class="col">
                                            <div class="" :class="{ 'card': !isSubPage }">
                                                <!-- page records template -->
                                                <div class="page-records" >
                                                    <DataTable :lazy="true" :loading="loading"    v-model:selection="selectedItems"
                                                         :value="records" dataKey="role_id" @sort="onSort($event)" class="" :showGridlines="false" :rowHover="true" responsiveLayout="stack">
                                                        <Column selectionMode="multiple" headerStyle="width: 3em"></Column>
                                                        <Column  field="" header="" >
                                                            <template #body="{data}">
                                                                <Button @click="setCurrentRecord(data)" class="full-width" icon="pi pi-plus" label="" />
                                                            </template>
                                                        </Column>
                                                        <Column  field="role_id" header="Role Id" >
                                                            <template #body="{data}">
                                                                <router-link :to="`/roles/view/${data.role_id}`">
                                                                    {{ data.role_id }}
                                                                </router-link>
                                                            </template>
                                                        </Column>
                                                        <Column  field="role_name" header="Role Name" >
                                                            <template #body="{data}">
                                                                {{ data.role_name }}
                                                            </template>
                                                        </Column>
                                                        <Column  headerStyle="width: 3em" headerClass="text-center">
                                                            <template #body="{data}">
                                                                <SplitButton class="p-button p-button-raised p-button-text p-button-sm" :model="getActionMenuModel(data)">
                                                                    <i></i>
                                                                </SplitButton>
                                                            </template>
                                                        </Column>
                                                    </DataTable>
                                                </div>
                                                <!-- page loading indicator -->
                                                <template v-if="loading">
                                                    <div class="flex align-items-center justify-content-center text-gray-500 p-3">
                                                        <div><ProgressSpinner style="width:30px;height:30px;" /> </div>
                                                        <div>Loading...</div>
                                                    </div>
                                                </template>
                                                <!-- end of page loading indicator-->
                                                <!-- Empty record -->
                                                <template v-if="pageReady && !records.length">
                                                    <div class="p-3 my-3 text-500 text-lg font-medium text-center">
                                                        No record found
                                                    </div>
                                                </template>
                                                <!-- end of empty record-->
                                                <!-- pagination component-->
                                                <template v-if="showFooter">
                                                    <div class="">
                                                        <div v-show="pageReady">
                                                            <div class="flex justify-content-between">
                                                                <div class="flex justify-content-center flex-grow-0">
                                                                    <template v-if="$auth.canView('roles/delete')">
                                                                        <div v-if="selectedItems.length" class="m-2">
                                                                            <Button @click="deleteItem(selectedItems)" icon="pi pi-trash" class="p-button-danger" title="Delete Selected" />
                                                                        </div>
                                                                    </template>
                                                                </div>
                                                                <div v-if="paginate && totalPages > 1" class="flex-grow-1">
                                                                    <Paginator class="border-none bg-transparent py-3" :first="recordsPosition" @page="(event)=>{pagination.page = event.page + 1}" :rows="pagination.limit" :totalRecords="totalRecords">
                                                                        <template #start>
                                                                            <span class="text-sm text-gray-500 px-2">
                                                                            Records <b>{{ recordsPosition }} of {{ totalRecords }}</b>
                                                                            </span>
                                                                        </template>
                                                                        <template #end>
                                                                        </template>
                                                                    </Paginator>
                                                                </div>
                                                            </div>  
                                                        </div>
                                                    </div>
                                                </template>
                                                <!-- end of pagination component-->
                                            </div>
                                        </div>
                                        <!-- Detal Page Column -->
                                        <template v-if="currentRecord && !isSubPage">
                                            <div class="col-12">
                                                <div class="card ">
                                                    <component :is="masterDetailPage" :scroll-into-view="true"></component>
                                                </div>
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
	import { defineAsyncComponent, computed,  reactive, toRefs, onMounted } from 'vue';
	import { useApp } from '@/composables/app.js';
	import { useAuth } from '@/composables/auth';
	import { useListPage } from '@/composables/listpage.js';
	const props = defineProps({
		primaryKey : {
			type : String,
			default : 'role_id',
		},
		pageName : {
			type : String,
			default : 'roles',
		},
		routeName : {
			type : String,
			default : 'roleslist',
		},
		apiPath : {
			type : String,
			default : 'roles/index',
		},
		paginate: {
			type: Boolean,
			default: true,
		},
		showHeader: {
			type: Boolean,
			default: true,
		},
		showFooter: {
			type: Boolean,
			default: true,
		},
		showBreadcrumbs: {
			type: Boolean,
			default: true,
		},
		exportButton: {
			type: Boolean,
			default: true,
		},
		importButton: {
			type: Boolean,
			default: false,
		},
		multiCheckbox: {
			type: Boolean,
			default: true,
		},
		emptyRecordMsg: {
			type: String,
			default: "No record found",
		},
		titleBeforeDelete: {
			type: String,
			default: "Delete record",
		},
		msgBeforeDelete: {
			type: String,
			default: "Are you sure you want to delete this record?",
		},
		msgAfterDelete: {
			type: String,
			default: "Record deleted successfully",
		},
		page: {
			type: Number,
			default: 1,
		},
		limit: {
			type: Number,
			default: 10,
		},
		mergeRecords: { // for infinite loading
			type: Boolean,
			default: false,
		},
		search: {
			type: String,
			default: '',
		},
		fieldName: null,
		fieldValue: null,
		sortBy: {
			type: String,
			default: '',
		},
		sortType: {
			type: String,
			default: '', //desc or asc
		},
		isSubPage: {
			type: Boolean,
			default: false,
		},
		filterTagClass: {
			type: String,
			default: 'surface-card p-2 text-500 flex-grow-1 text-center m-1 mb-3 flex-grow-1 text-center',
		}
	});
	const app = useApp();
	const auth = useAuth();
	const filters = reactive({
	});
	//init list page hook
	const page = useListPage(props, filters);
	//page state
	const { 
		totalRecords, // total records from api - Number
		recordCount, // current record count - Number
		loading, // Api loading state - Boolean
		selectedItems, // Data table selected items -Array
		pagination, //Pagination object - Object
		searchText, // search text - String
		pageReady, // api data loaded successfully
	} = toRefs(page.state);
	//page computed propties
	const { 
		records, // page record from store - Array
		apiUrl, // current api path - URL
		currentRecord, // master detail selected record - Object
		pageBreadCrumb, // get page navigation paths - Object
		canLoadMore, // if api has more data for loading - Boolean
		finishedLoading, // if api has finished loading - Boolean
		totalPages, // total number of pages from api - Number
		recordsPosition // current record position from api - Number
	} = page.computedProps
	//page methods
	const {
		load, // load data from api
		reload, // reset pagination and load data
		loadNextPage, // load next page
		loadPreviousPage, // load previous page
		exportPage, // export page records - args = (exportFormats, apiUrl, pageName)
		clearSearch, // clear input search
		debounce, // debounce input search
		onSort, // reset page number before sort from api
		deleteItem, // delete item by id or selected items - args = (id) or (selectedItems)
		setCurrentRecord, // master detail set current record
		isCurrentRecord, // master detail 
		removeFilter, // remove page filter item - args = (filter.propertyname)
		getFilterLabel, // get filter item display label - args = (filter.propertyname)
		filterHasValue, //check if filter item has value - args = (filter.propertyname)
		importComplete // reload page after data import
	} = page.methods
	function getActionMenuModel(data){
		return [
		{
			label: "View",
			to: `/roles/view/${data.role_id}`,
			icon: "pi pi-eye",
			visible: auth.canView('roles/view')
		},
		{
			label: "Edit",
			command: (event) => { app.openPageDialog({ page:'roles/edit', url: `/roles/edit/${data.role_id}` , closeBtn: true }) },
			icon: "pi pi-pencil",
			visible: auth.canView('roles/edit')
		},
		{
			label: "Delete",
			command: (event) => { deleteItem(data.role_id) },
			icon: "pi pi-minus-circle",
			visible: auth.canView('roles/delete')
		}
	]
	}
	const masterDetailPage = computed(() => defineAsyncComponent(() => import("./detail-pages.vue")));
	onMounted(()=>{ 
		const pageTitle = "Roles";
		app.setPageTitle(props.routeName, pageTitle);
		load();
	});
</script>
<style scoped>
</style>
