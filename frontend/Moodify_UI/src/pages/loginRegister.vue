<template>
    
    <div class="w-100 min-vh-100 border border-1 border-dark d-flex flex-row justify-content-center">
        <div class="w-100 border border-2 border-danger">
            <LogInForm
                @loginUser = "logIn"
                @spotifyLogin = "spotifyLogin"
                @toggleIsLoading="toggleIsLoading"
                @setError="setError"
                :isLoading="loginIsLoading"
                :error="loginError"
                :response="loginResponse"
            />
        </div>
        <div class="w-100 border border-2 border-danger">
            <SignInForm
                @registerUser = "signIn"
                @toggleIsLoading="toggleIsLoading"
                @setError="setError"
                :isLoading="signinIsLoading"
                :error="signinError"
                :response="signinResponse"
            />
        </div>
    </div>

</template>

<script>

import LogInForm from '../components/logInForm.vue';
import SignInForm from '../components/signInForm.vue';
import authService from '../services/authService'

export default {
    name : "LoginRegister",
    components: {
        SignInForm,
        LogInForm
    },
    data(){
        return{
            loginIsLoading : false,
            loginError: '',
            loginResponse: null,
            signinIsLoading : false,
            signinError: '',
            signinResponse: null,
        }
    },
    methods:{
        async signIn(user){
            try{
                this.signinIsLoading = true;
                this.signinResponse = await authService.registerUserAsync(user);
                //console.log(this.signinResponse);

                
                if(this.signinResponse){
                    this.setError("signIn", "");
                    this.toggleIsLoading("signIn", false);
                    this.$router.push("/home");
                }
            }
            catch(err){
                this.signinError = err.response.data.message;
                this.signinIsLoading = false;
            }
            
        },
        async logIn(user){

            try{
                this.loginIsLoading = true;
                this.loginResponse = await authService.loginUserAsync(user);
                console.log(this.loginResponse.data);


                if(this.loginResponse){
                    this.setError("logIn", "");
                    this.toggleIsLoading("logIn", false);
                    this.$router.push("/home");
                }
    
            }
            catch(err){
                //console.log(err.response.data.message);
                this.loginError = err.response.data.message;
                this.loginIsLoading = false;
            }
            
           
        },
        async spotifyLogin(){
            try{
                await authService.spotifyLogin();
            }
            catch(err){
                console.log(err);
            }
        },
        toggleIsLoading(location, isLoading){
            //console.log("toggle clicked")

            if(location == "signIn"){
                this.signinIsLoading = isLoading;
            }

            if(location == "logIn"){
                this.loginIsLoading = isLoading;
            }
            
        },
        setError(location, err){

             if(location == "signIn"){
                this.signinError = err
             }
             if(location == "logIn"){
                this.loginError = err
             }
        }
        
    }
}

</script>

<style scoped>

</style>