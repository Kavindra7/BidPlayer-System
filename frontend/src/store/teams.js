

import { ApiService } from '@/services/api'
export const teams = {
	namespaced: true,
	state: {
		currentRecord: null,
		records: [],
		requestError: null,
		formData: {},
	},
	getters: {
		records(state) {
			return state.records
		},
		currentRecord(state) {
			return state.currentRecord
		},
		formData(state) {
			return state.formData
		},
	},
	mutations: {
		setRecords(state, records) {
			state.records = records
		},
		setCurrentRecord(state, record) {
			state.currentRecord = record;
		},
		setFormData(state, formData) {
			state.formData = formData;
		},
		mergeRecords(state, records) {
			state.records = state.records.concat(records);
		},
		addRecord(state, record) {
			if(Array.isArray(record)){
				for (let index = 0; index < record.length; index++) {
					state.records.unshift(record[index]);
				}
			}
			else{
				state.records.unshift(record);
				state.currentRecord = record;
			}
		},
		updateRecord(state, record) {
			const item = state.records.find(item => item['id'] === record['id']);
			if(item){
				Object.assign(item, record);
				state.currentRecord = item;
			}
		},
		deleteRecord(state, id) {
			if(Array.isArray(id)){
				id.forEach((itemId) => {
					let itemIndex = state.records.findIndex(item => item['id'] == itemId);
					if(itemIndex!=-1){
						state.records.splice(itemIndex, 1);
					}
				})
			}
			else{
				let itemIndex = state.records.findIndex(item => item['id'] == id);
				if(itemIndex!=-1){
					state.records.splice(itemIndex, 1);
				}
			}
		},
		setError(state, errors) {
			state.requestError = error
		},
	},
	actions: {
		fetchRecords: async ({ commit }, args) => {
			try {
				const { url, merge } = args;
				const response = await ApiService.get(url);
				const apiData = response.data;
				const records = apiData.records || apiData;
				if (merge) {
					commit("mergeRecords", records);
				}
				else {
					commit("setRecords", records);
				}
				return apiData;
			}
			catch (err) {
				throw err;
			}
		},
		fetchRecord: async ({ commit }, url) => {
			try {
				const response = await ApiService.get(url);
				const apiData = response.data;
				commit("setCurrentRecord", apiData);
				return apiData;
			}
			catch (err) {
				throw err;
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
		updateRecord: async ({ commit }, args) => {
			try {
				const { url, payload } = args;
				const response = await ApiService.post(url, payload);
				const record = response.data;
				commit("updateRecord", record);
				return record;
			}
			catch (err) {
				throw err;
			}
		},
		deleteRecord: async ({ commit }, args) => {
			try {
				const { url, id } = args;
				const response = await ApiService.get(url);
				const record = response.data;
				commit("deleteRecord", id);
				return record;
			}
			catch (err) {
				throw err;
			}
		},
	}
}
