var userWiseTrophy;
export const AppMenus = {
    
	navbarTopRightItems: [],
	navbarTopLeftItems: [],
	navbarSideLeftItems: [
  {
    "to": "/home",
    "label": "Home",
    "icon": "pi pi-th-large text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/trophies",
    "label": "Trophies",
    "icon": "pi pi-heart text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/teams",
    "label": "Teams",
    "icon": "pi pi-star-fill text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/bid_players",
    "label": "Auctions",
    "icon": "pi pi-money-bill text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/my_players",
    "label": "My Players",
    "icon": "pi pi-users text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/users",
    "label": "Users",
    "icon": "pi pi-user-edit text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/roles",
    "label": "Roles",
    "icon": "pi pi-th-large text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/permissions",
    "label": "Permissions",
    "icon": "pi pi-th-large text-primary",
    "iconcolor": "",
    "target": "",
    
  },
  {
    "to": "/register_with_trophy",
    "label": "Reg With Trophy/Bid Players",
    "icon": "pi pi-th-large text-primary",
    "iconcolor": "",
    "target": "",
    
  }
],
	statusItems: [    
{value: "Active", label: "Active"},
	{value: "Inactive", label: "Inactive"}
    ],
	genderItems: [    
{value: "Male", label: "Male"},
	{value: "Female", label: "Female"}
    ],

    exportFormats: {
        print: {
			label: 'Print',
            icon: 'pi pi-print',
            type: 'print',
            ext: '',
        },
        pdf: {
			label: 'Pdf',
			
            icon: 'pi pi-file-pdf',
            type: 'pdf',
            ext: 'pdf',
        },
        excel: {
			label: 'Excel',
            icon: 'pi pi-file-excel',
            type: 'excel',
            ext: 'xlsx',
        },
        csv: {
			label: 'Csv',
            icon: 'pi pi-table',
            type: 'csv',
            ext: 'csv',
        },
    },
    
}