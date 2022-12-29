<template>
    <div class="container">
        <div class="text-center card">
            <Avatar size="large" class="bg-green-500 text-white" icon="pi pi-check-circle" />    
            <div class="text-4xl my-3 font-bold text-green-500 my-3">
                Congratulations!
            </div>
            <div class="text-2xl text-primary">
                Your account has been created.
            </div>
            <hr />
            <router-link to="/">
                <Button label="Continue" icon="pi pi-home" />
            </router-link>
        </div>
    </div>
</template>
