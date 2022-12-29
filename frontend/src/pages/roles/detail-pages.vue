<template>
    <div id="master-detailpage">
        <TabView v-model:activeIndex="activeTab">
            <template v-if="$auth.canView('permissions/list')">
                <TabPanel>
                    <template #header>
                        <div class=" text-lg font-bold" >
                            Roles Permissions
                        </div>
                    </template>
                    <div>
                        <PermissionsListPage ref="permissionsListPage"  field-name="role_id" :field-value="masterRecord.role_id" :show-header="true" v-if="pageReady">
                        </PermissionsListPage>
                    </div>
                </TabPanel>
            </template>
            <template v-if="$auth.canView('users/list')">
                <TabPanel>
                    <template #header>
                        <div class=" text-lg font-bold" >
                            Roles Users
                        </div>
                    </template>
                    <div>
                        <UsersListPage ref="usersListPage"  field-name="user_role_id" :field-value="masterRecord.role_id" :show-header="true" v-if="pageReady">
                        </UsersListPage>
                    </div>
                </TabPanel>
            </template>
        </TabView>
    </div>
</template>
<script setup>
import { watch, computed, ref, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useApp } from '@/composables/app.js';
import { useAuth } from '@/composables/auth';
import PermissionsListPage from "../permissions/list.vue";
import UsersListPage from "../users/list.vue";
const props = defineProps({
	isSubPage: {
		type : Boolean,
		default : true,
	},
	scrollIntoView: {
		type : Boolean,
		default : false,
	},
});
const app = useApp();
const auth = useAuth();
const store = useStore();
const masterRecord = computed(() => store.getters['roles/currentRecord']);
const activeTab = ref(0);
const pageReady = ref(true);
//scroll detail page into view
function scrollToDetailPage() {
	if (props.scrollIntoView) {
		const pageElement = document.getElementById('master-detailpage');
		if(pageElement){
			pageElement.scrollIntoView({behavior:'smooth', block:'start'});
		}
	}
}
// pass form data from master to detail
function setDetailPageFormData(){
	const record = masterRecord.value;
	// set  form data
	const permissionsFormData = { role_id:record?.role_id }
	store.commit('permissions/setFormData', permissionsFormData);
	// set  form data
	const usersFormData = { user_role_id:record?.role_id }
	store.commit('users/setFormData', usersFormData);
}
watch(() => masterRecord, (current) => {
		setDetailPageFormData();
		scrollToDetailPage();
	},
	{ deep: true, immediate: true }
);
onMounted(()=>{ 
    scrollToDetailPage();
});
</script>
