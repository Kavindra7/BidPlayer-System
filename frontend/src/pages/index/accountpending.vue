<template>
    <div class="container">
        <div class="text-center card">
            <Avatar size="large" class="bg-orange-500 text-white" icon="pi pi-user" /> 
            <div class="text-4xl font-bold text-orange-500 my-3">
                Your account is waiting for review
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
