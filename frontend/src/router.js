
//Copyright - My Esoft Project - Do not edit!
import { createRouter, createWebHashHistory } from 'vue-router';
export default ({ store, auth }) => {
	let routes = [
		//Dashboard routes


//bid_players routes
			{
				path: '/bid_players/:index?/:fieldName?/:fieldValue?',
				name: 'bid_playerslist',
				component: () => import('./pages/bid_players/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/bid_players/view/:id', 
				name: 'bid_playersview', 
				component: () => import('./pages/bid_players/view.vue'), 
				props: true
			},
	
			{ 
				path: '/bid_players/add', 
				name: 'bid_playersadd', 
				component: () => import('./pages/bid_players/add.vue'), 
				props: true
			},
	
			{ 
				path: '/bid_players/edit/:id', 
				name: 'bid_playersedit', 
				component: () => import('./pages/bid_players/edit.vue'), 
				props: true
			},
		

//my_players routes
			{
				path: '/my_players/:index?/:fieldName?/:fieldValue?',
				name: 'my_playerslist',
				component: () => import('./pages/my_players/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/my_players/view/:id', 
				name: 'my_playersview', 
				component: () => import('./pages/my_players/view.vue'), 
				props: true
			},
	
			{ 
				path: '/my_players/add', 
				name: 'my_playersadd', 
				component: () => import('./pages/my_players/add.vue'), 
				props: true
			},
	
			{ 
				path: '/my_players/edit/:id', 
				name: 'my_playersedit', 
				component: () => import('./pages/my_players/edit.vue'), 
				props: true
			},
		

//permissions routes
			{
				path: '/permissions/:index?/:fieldName?/:fieldValue?',
				name: 'permissionslist',
				component: () => import('./pages/permissions/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/permissions/view/:id', 
				name: 'permissionsview', 
				component: () => import('./pages/permissions/view.vue'), 
				props: true
			},
	
			{ 
				path: '/permissions/add', 
				name: 'permissionsadd', 
				component: () => import('./pages/permissions/add.vue'), 
				props: true
			},
	
			{ 
				path: '/permissions/edit/:id', 
				name: 'permissionsedit', 
				component: () => import('./pages/permissions/edit.vue'), 
				props: true
			},
		

//register_with_trophy routes
			{
				path: '/register_with_trophy/:index?/:fieldName?/:fieldValue?',
				name: 'register_with_trophylist',
				component: () => import('./pages/register_with_trophy/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/register_with_trophy/view/:id', 
				name: 'register_with_trophyview', 
				component: () => import('./pages/register_with_trophy/view.vue'), 
				props: true
			},
	
			{ 
				path: '/register_with_trophy/add', 
				name: 'register_with_trophyadd', 
				component: () => import('./pages/register_with_trophy/add.vue'), 
				props: true
			},
	
			{ 
				path: '/register_with_trophy/edit/:id', 
				name: 'register_with_trophyedit', 
				component: () => import('./pages/register_with_trophy/edit.vue'), 
				props: true
			},
		

//roles routes
			{
				path: '/roles/:index?/:fieldName?/:fieldValue?',
				name: 'roleslist',
				component: () => import('./pages/roles/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/roles/view/:id', 
				name: 'rolesview', 
				component: () => import('./pages/roles/view.vue'), 
				props: true
			},
	
			{ 
				path: '/roles/add', 
				name: 'rolesadd', 
				component: () => import('./pages/roles/add.vue'), 
				props: true
			},
	
			{ 
				path: '/roles/edit/:id', 
				name: 'rolesedit', 
				component: () => import('./pages/roles/edit.vue'), 
				props: true
			},
		

//teams routes
			{
				path: '/teams/:index?/:fieldName?/:fieldValue?',
				name: 'teamslist',
				component: () => import('./pages/teams/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/teams/view/:id', 
				name: 'teamsview', 
				component: () => import('./pages/teams/view.vue'), 
				props: true
			},
	
			{ 
				path: '/teams/add', 
				name: 'teamsadd', 
				component: () => import('./pages/teams/add.vue'), 
				props: true
			},
	
			{ 
				path: '/teams/edit/:id', 
				name: 'teamsedit', 
				component: () => import('./pages/teams/edit.vue'), 
				props: true
			},
		

//trophies routes
			{
				path: '/trophies/:index?/:fieldName?/:fieldValue?',
				name: 'trophieslist',
				component: () => import('./pages/trophies/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/trophies/view/:id', 
				name: 'trophiesview', 
				component: () => import('./pages/trophies/view.vue'), 
				props: true
			},
	
			{ 
				path: '/trophies/add', 
				name: 'trophiesadd', 
				component: () => import('./pages/trophies/add.vue'), 
				props: true
			},
	
			{ 
				path: '/trophies/edit/:id', 
				name: 'trophiesedit', 
				component: () => import('./pages/trophies/edit.vue'), 
				props: true
			},
		

//users routes
			{
				path: '/users/:index?/:fieldName?/:fieldValue?',
				name: 'userslist',
				component: () => import('./pages/users/list.vue'), 
				props: true
			},
			
	
			{ 
				path: '/users/view/:id', 
				name: 'usersview', 
				component: () => import('./pages/users/view.vue'), 
				props: true
			},
	
			{ 
				path: '/index/register', 
				name: 'usersuserregister', 
				component: () => import('./pages/index/userregister.vue'), 
				props: true
			},
	
			{ 
				path: '/account/edit', 
				name: 'usersaccountedit', 
				component: () => import('./pages/account/accountedit.vue'), 
				props: true
			},
	
			{ 
				path: '/account', 
				name: 'usersaccountview', 
				component: () => import('./pages/account/accountview.vue'), 
				props: true
			},
	
			{ 
				path: '/users/add', 
				name: 'usersadd', 
				component: () => import('./pages/users/add.vue'), 
				props: true
			},
	
			{ 
				path: '/users/edit/:id', 
				name: 'usersedit', 
				component: () => import('./pages/users/edit.vue'), 
				props: true
			},
		

		
		
//Password reset routes
			{ 
				path: '/index/forgotpassword', 
				name: 'forgotpassword', 
				component: () => import('./pages/index/forgotpassword.vue'), 
				props: true
			},
			{ 
				path: '/index/resetpassword', 
				name: 'resetpassword', 
				component: () => import('./pages/index/resetpassword.vue'), 
				props: true
			},
			{ 
				path: '/index/resetpassword_completed', 
				name: 'resetpassword_completed', 
				component: () => import('./pages/index/resetpassword_completed.vue'), 
				props: true
			},
	
		
		
		{ 
			path:  '/error/forbidden', 
			name: 'forbidden', 
			component: () => import('./pages/errors/forbidden.vue'),
			props: true
		},
		{ 
			path: '/error/notfound', 
			name: 'notfound',
			component: () => import('./pages/errors/pagenotfound.vue'),
			props: true
		},
		{
			path: '/:catchAll(.*)', 
			component: () => import('./pages/errors/pagenotfound.vue')
		}
	];
	
	let user = store.state.auth.user;
	if(user){
		let userRole = store.getters["auth/getUserRole"].toLowerCase();
		let rolePage;
		switch(userRole){
			case "player":
				rolePage = "player";
				break;
			case "team_owner":
				rolePage = "team_owner";
				break;
			default:
				rolePage = "home";
		}
		
		//Dashboard route
		//Display page based on user role
		routes.push({
			path: '/',
			alias: '/home', 
			name: 'home', 
			component: () => import(`./pages/home/${rolePage}.vue`),
			props: true
		});
	}
	else{
		routes.push({
			path: '/', 
			alias: '/index', 
			name: 'index', 
			component: () => import('./pages/index/index.vue'),
			props: true
		});
	}


	const router = createRouter({
		history: createWebHashHistory(),
		routes,
		scrollBehavior(to, from, savedPostion){
			//if(savedPostion) return savedPostion;
			return { x:0, y:0 }
		}
	});

	
	router.beforeEach((to, from, next) => {
		const user = store.state.auth.user;
		let path = to.path;
	
		let authRequired = auth.pageRequiredAuth(path);
		if (authRequired) {
			if(!user){
				return next({ path: '/',  query: { nexturl: to.fullPath } });
			}
			//authorize user
			if (!auth.canView(path)) {
				return next({path: "/error/forbidden"});
			}
		}

		if(user && to.name == "index"){
			//already logged in, show home when try to access index page
			return next({ path: "/home"});
		}

		//navigate to redirect url if available
		if(to.name == "home" && to.query.nexturl){
			return next({ path: to.query.nexturl});
		}

 		//close dialog from previous page
		store.dispatch('app/closeDialogs');
		
		
		next();
	});

	return router;
}
