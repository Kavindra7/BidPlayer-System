<template>
    <div class="grid justify-content-center">
        <div class="col-12 md:col-6">
            <div class="card my-4 text-center">
                <div class="text-2xl font-bold text-success">
                    <Avatar icon="pi pi-check-circle" size="large" />
                    Your password has been changed successfully
                </div>
                <hr  />
                <router-link to="/">
                    <Button class="p-button-text" label="Click here to login" />
                </router-link>
            </div>
        </div>
    </div>
</template>