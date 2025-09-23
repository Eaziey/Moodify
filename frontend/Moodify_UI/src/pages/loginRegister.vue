<template>
    
    <div class="w-100 h-100 border border-1 border-dark d-flex flex-row justify-content-center">
        <div class="container w-100 border border-2 border-danger text-center p-2">
            <h1 class="text-primary">Moodify</h1>
        </div>
        <div class="container">
            <SignInForm/>
        </div>
    </div>

</template>

<script>

import SignInForm from '../components/signInForm.vue';

export default {
    name : "LoginRegister",
    components: {
        SignInForm
    }
}

</script>

<style scoped>

</style>