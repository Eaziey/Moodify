<template>
    
    <div class="w-100 min-vh-100 border border-1 border-dark d-flex flex-row justify-content-center">
        <div class="w-100 border border-2 border-danger">
            <LogInForm/>
        </div>
        <div class="w-100 border border-2 border-danger">
            <SignInForm/>
        </div>
    </div>

</template>

<script>

import LogInForm from '../components/logInForm.vue';
import SignInForm from '../components/signInForm.vue';

export default {
    name : "LoginRegister",
    components: {
        SignInForm,
        LogInForm
    }
}

</script>

<style scoped>

</style>