<template>
    <div class="container">
        <div class="text-center card">
            <Avatar class="bg-pink-500 text-white" size="large" icon="pi pi-ban" />   
            <div class="text-4xl my-3  font-bold text-pink-500 my-3">
                Your account has been blocked
            </div>
            <div class="text-primary">
                Please contact the system administrator for more information
            </div>
            <hr />
            <router-link to="/">
                <Button label="Continue" icon="pi pi-home" />
            </router-link>
        </div>
    </div>
</template>
