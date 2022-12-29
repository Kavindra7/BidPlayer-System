<template>
    <div :class="containerClass" @click="onWrapperClick">
        <template v-if="$auth.isLoggedIn">
            <div class="layout-topbar bg-primary shadow-7">
                <router-link to="/" class="layout-topbar-logo flex-grow-none">
                    <img src="images/logo.png" alt="logo" class="my-5" />
                    <span class="text-white">{{ $appName }}</span>
                </router-link>
                <div class="layout-topbar-menu flex-grow-1 justify-content-between">
                    <button class="p-link layout-topbar-button text-white layout-topbar-menu-button" @click="onMenuToggle">
                    <i class="pi pi-bars"></i>
                    </button>
                    <div class="flex">
                        <template v-for="(item, i) of navbarTopLeftItems" :key="i">
                            <router-link :to="item.to">
                                <Button :label="item.label" :icon="item.icon" class="p-button-text text-white page-button" />
                            </router-link>
                        </template>
                    </div>
                    <SplitButton class="layout-menu-user-button"  icon="pi pi-user" :model="appBarActionMenu">
                        <router-link to="/account">
                            <Avatar shape="circle" class="bg-primary text-white" size="large" icon="pi pi-user" />
                            </router-link>
                        </SplitButton>
                    </div>
                </div>
                <div class="layout-sidebar" @click="onSidebarClick">
                    <div class="text-center p-4 surface-100 mb-3">
                        <router-link to="/account">
                            <Avatar size="xlarge" shape="circle" icon="pi pi-user" />
                            </router-link>
                            <div class="text-primary font-bold text-center py-2">
                                Hi {{ $auth.userName }}
                                <div class="text-sm text-500 text-capitalize py-2" v-if="$auth.userRole">{{ $auth.userRole}}</div>
                            </div>
                        </div>
                        <AppMenu :model="navbarSideLeftItems" @menuitem-click="onMenuItemClick" />
                        </div>
                    </template>
                    <template v-else>
                        <div class="layout-topbar bg-primary text-white shadow-7">
                            <router-link to="/" class="layout-topbar-logo">
                                <img src="images/logo.png" alt="logo" class="my-5" />
                                <span class="text-white">{{ $appName }}</span>
                            </router-link>
                        </div>
                    </template>
                    <div class="layout-main-container">
                        <div class="layout-main">
                            <router-view />
                        </div>
                    </div>
                    <!-- App right drawer -->
                    <Sidebar :showCloseIcon="false" position="right" v-model:visible="showRightDrawer" :style="{width: rightDrawer.width}">
                        <component v-if="showRightDrawer" is-sub-page :is="drawerComponentFile" :api-path="rightDrawer.pageUrl" :page-data="rightDrawer.pageData" />
                    </Sidebar>
                    <transition name="layout-mask">
                        <div class="layout-mask p-component-overlay" v-if="mobileMenuActive"></div>
                    </transition>
                    <!-- Page display dialog -->
                    <Dialog class="card py-3 px-0" :breakpoints="{'960px': '50vw', '640px': '95vw'}" :style="{width: pageDialog.width, maxWidth: pageDialog.maxWidth}" :modal="!pageDialog.seamless" :maximized="pageDialog.maximized" :dismissableMask="!pageDialog.persistent" :position="pageDialog.position" v-model:visible="showPageDialog" :showHeader="false">
                        <Button v-if="pageDialog.closeBtn" @click="showPageDialog=false" style="position:absolute;right:15px;top:5px" icon="pi pi-times" class="p-button-rounded p-button-text p-button-plain" />
                        <component  v-if="pageDialog.showDialog" is-sub-page :is="dialogComponentFile" :api-path="pageDialog.pageUrl" :page-data="pageDialog.pageData" />
                    </Dialog>
                    <!-- image preview dialog-->
                    <Dialog class="card" :showHeader="false" :breakpoints="{'960px': '40vw', '640px': '95vw'}" style="width:40vw" v-model:visible="showImageDialog" dismissableMask modal>
                        <Carousel :value="imageDialog.images" :circular="false" :page="imageDialog.currentSlide">
                        <template #item="{data}">
                            <div class="text-center">
                                <img style="max-width:100%;max-height:80%;" :src="data" />
                            </div>
                        </template>
                        </Carousel>
                    </Dialog>
                    <!-- request error dialog -->
                    <Dialog class="card p-0" modal  v-model:visible="errorDialog" :breakpoints="{'960px': '50vw', '640px': '95vw'}" style="width:30vw;" position="top">
                        <template #header>
                            <div class="flex align-items-center">
                                <div class="mr-2">
                                    <Avatar size="large" class="bg-pink-100 text-pink-600" icon="pi pi-exclamation-triangle" />
                                    </div>
                                    <div class="flex text-lg text-pink-600 font-bold">
                                        Unable to complete request.
                                    </div>
                                </div>
                            </template>
                            <div class="text-grey-500 font-bold text-lg" v-for="(msg, index) in pageErrors" :key="index">
                                {{msg}}
                            </div>
                        </Dialog>
                        <ConfirmDialog></ConfirmDialog>
                        <Toast position="top-center" />
                        <Dialog class="card m-0 text-center" :showHeader="false" modal  v-model:visible="fullScreenLoading" :breakpoints="{'960px': '50vw', '640px': '95vw'}" style="width:10vw;" position="center">
                            <ProgressSpinner style="width:60px" /> 
                            {{ fullScreenLoadingMsg }}
                        </Dialog>
                    </div>
</template>
<script setup>
	import { defineAsyncComponent, ref, computed } from "vue";
	import AppMenu from '@/components/AppMenu.vue';
	import { useApp } from '@/composables/app';
	import { useAuth } from '@/composables/auth';
	import { useRoute, useRouter } from 'vue-router';
	import { ApiService } from '@/services/api';
	import { useStore } from 'vuex'

	const app = useApp();
	const auth = useAuth();
	const store = useStore();
	const route = useRoute();
	const router = useRouter();
	const pageDialog = computed(() =>  store.state.app.pageDialog );
	const imageDialog = computed(() =>  store.state.app.imageDialog );
	const rightDrawer = computed(() =>  store.state.app.rightDrawer );

	const showPageDialog = computed ({
		get() {
			return pageDialog.value.showDialog
		},
		set(value) {
			store.dispatch("app/closePageDialog");
		},
	});

	const showImageDialog = computed ({
		get() {
			return imageDialog.value.showDialog
		},
		set(value) {
			store.dispatch("app/closeImageDialog");
		},
	});

	const showRightDrawer = computed ({
		get() {
			return rightDrawer.value.showDrawer;
		},
		set(value) {
			store.dispatch("app/closeRightDrawer");
		},
	});
	const pageErrors = computed ({
		get() {
			return store.state.app.pageErrors;
		},
		set(value) {
			store.commit("app/setPageErrors", value);
		},
	});

	const errorDialog = computed ({
		get() {
			return pageErrors.value.length > 0;
		},
		set(newValue) {
			pageErrors.value = [];
		}
	});
	const fullScreenLoading = computed(() =>  store.state.app.fullScreenLoading );
	const fullScreenLoadingMsg = computed(() =>  store.state.app.fullScreenLoadingMsg );

	const dialogComponentFile = computed(() => {
		const dialog = pageDialog.value;
		if(dialog.showDialog && dialog.pageComponent){
			return defineAsyncComponent(() => import(`@/pages/${dialog.pageComponent}.vue`));
		}
		return null;
	});
	const drawerComponentFile = computed(() => {
		const drawer = rightDrawer.value;
		if(drawer.showDrawer && drawer.pageComponent){
			return defineAsyncComponent(() => import(`@/pages/${drawer.pageComponent}.vue`));
		}
		return null;
	});
	const layoutMode = ref('static');
	const menuClick = ref(false);
	const staticMenuInactive = ref(false);
	const overlayMenuActive = ref(false);
	const mobileMenuActive = ref(false);

	const containerClass = computed(() => {
		if(!auth.isLoggedIn){
			staticMenuInactive.value = true;
			mobileMenuActive.value = false;
		}

		return ['layout-wrapper', {
			'layout-overlay': layoutMode.value === 'overlay',
			'layout-static': layoutMode.value === 'static',
			'layout-static-sidebar-inactive': staticMenuInactive.value && layoutMode.value === 'static',
			'layout-overlay-sidebar-active': overlayMenuActive.value && layoutMode.value === 'overlay',
			'layout-mobile-sidebar-active': mobileMenuActive.value,
			'p-input-filled': false,
		}];
	});
	const navbarSideLeftItems = app.menus.navbarSideLeftItems.filter((menu) => auth.canView(menu.to));
	const navbarTopLeftItems = app.menus.navbarTopLeftItems.filter((menu) => auth.canView(menu.to));
	const navbarTopRightItems = app.menus.navbarTopRightItems.filter((menu) => auth.canView(menu.to));
	function onMenuItemClick(event) {
		if (event.item && !event.item.items) {
			overlayMenuActive.value = false;
			mobileMenuActive.value = false;
		}
	}

	function onWrapperClick() {
		if (!menuClick.value) {
			overlayMenuActive.value = false;
			mobileMenuActive.value = false;
		}
		menuClick.value = false;
	}

	function onMenuToggle() {
		menuClick.value = true;
		if (app.isDesktop()) {
			if (layoutMode.value === 'overlay') {
				if(mobileMenuActive.value === true) {
					overlayMenuActive.value = true;
				}
				overlayMenuActive.value = !overlayMenuActive.value;
				mobileMenuActive.value = false;
			}
			else if (layoutMode.value === 'static') {
				staticMenuInactive.value = !staticMenuInactive.value;
			}
		}
		else {
			mobileMenuActive.value = !mobileMenuActive.value;
		}
	}

	const  appBarActionMenu = [
		{
			to: '/account',
			label: "My Account",
			icon: 'pi pi-user',
		},
		{
			label: "Logout",
			icon: 'pi pi-window-minimize',
			command: (event) => {
				startLogout();
			},
		},
	];
	function startLogout() {
		auth.logout();
		location.href = "/"; //reload page and navigated to index page
	}
	function initAxioInterceptors(){
		// Add a request interceptor
		ApiService.axios().interceptors.request.use(
			(config) => {
				//before new request, hide previous error message
				pageErrors.value = [];
				return config;
			},
			(error) => {
			// Do something with request error
			return Promise.reject(error);
		});
	}
	initAxioInterceptors();
</script>

<style lang="scss">
@import './App.scss';
</style>
