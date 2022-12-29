import { createStore } from "vuex"
import { app } from "./app.js"
//import appStore from "./app_store.js";
import { auth } from "./auth.js"
import { bid_players } from "./bid_players.js"
import { my_players } from "./my_players.js"
import { permissions } from "./permissions.js"
import { register_with_trophy } from "./register_with_trophy.js"
import { roles } from "./roles.js"
import { teams } from "./teams.js"
import { trophies } from "./trophies.js"
import { users } from "./users.js"

//const { state, getters, mutations, actions } = appStore;

const store = createStore({
	modules: {
		app,
		auth,
		bid_players,
		my_players,
		permissions,
		register_with_trophy,
		roles,
		teams,
		trophies,
		users
	},
	// enable strict mode (adds overhead!)
	// for dev mode only
	//strict: process.env.DEV
});
export default store;