
import { ApiService } from '@/services/api'
import { StorageService } from '../services/storage'
export const auth = {
	namespaced: true,
	state: {
		user: null,
		userPages: [],
		userRoles: [],
	},
	getters: {
		getLoginToken(state) {
			let token = StorageService.getToken() || null;
			return token;
		},
		getUserRole(state) { // return default role
			if(state.userRoles.length) {
				return state.userRoles[0];
			}
			return "";
		}
	},
	mutations: {
		
		setUserData(state, userData) {
			if(userData){
				state.user = userData.user || null
				state.userPages = userData.pages || []
				state.userRoles = userData.roles || []
			}
		},

	},
	actions: {
		getUserData: async ({ commit }, args) => {
			try {
				const response = await ApiService.get("account/currentuserdata");
				const userData = response.data;
				commit("setUserData", userData);
				return userData;
			}
			catch (err) {
				throw err;
			}
		},
		login: async ({ commit, dispatch }, args) => {
			try {
				const { url, formData, rememberUser } = args;
				const response = await ApiService.post(url, formData);
				const loginData = response.data;
				dispatch('saveLoginData', { loginData, rememberUser });
				return loginData;
			}
			catch (err) {
				throw err;
			}
		},
		saveLoginData: async ({ commit }, args) => {
			const { loginData, rememberUser } = args;
			if(loginData.user && loginData.token){
				StorageService.saveLoginData(loginData, rememberUser);
			}
		},
		saveRecord: async ({ commit }, args) => {
			try {
				const { url, payload } = args;
				const response = await ApiService.post(url, payload);
				const record = response.data;
				commit("addRecord", record);
				return record;
			}
			catch (err) {
				throw err;
			}
		},
		logout: ({ commit }) => {
			// Remove the token and remove Authorization header from Api Service as well 
			StorageService.removeLoginData();
			ApiService.removeHeader();
		},
	}
}
