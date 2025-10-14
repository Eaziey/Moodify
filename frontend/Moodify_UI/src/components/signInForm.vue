<template>

<div class="py-5 border border-primary w-100 d-flex justify-content-center align-items-center">
    <form class="border d-flex flex-column p-5 h-75 w-75">
        <h1 class="text-primary align-self-center mb-5">Register</h1>

        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input
                type="text" 
                class="form-control p-2" 
                placeholder="Username"
                v-model="user.username"
            />
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input 
                type="email" 
                class="form-control p-2" 
                placeholder="Email"
                v-model = "user.email"
            />
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input 
                type="password" 
                class="form-control border-end-0 p-2" 
                placeholder="Password"
                v-model = "pw"
            />
            <span class="input-group-text border-start-0 bg-transparent hover-pointer p-2">#</span>
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input 
                type="password" 
                class=" form-control border-end-0 p-2" 
                placeholder="Confirm Password"
                v-model = "confirmPw"
            />
            <span class="input-group-text border-start-0 bg-transparent hover-pointer p-2">#</span>
        </div>
        <p class="text-danger">{{ error }}</p>
        <button 
            type="submit" 
            class="btn btn-outline-success rounded-2 w-50 align-self-center mt-4"
            @click="OpenModal">
                <div v-if="isLoading" id="spinner" class="text-center">
                  <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                  </div>
                </div>
                <p v-else>Sign In</p>
        </button>
        <SpotifySignInModal 
            ref="spotifySignInModal"
            @integrate = "SetIntergrateAndSignIn"
        />
        
        <div class="d-flex flex-row justify-content-center mt-5">
            <p class="align-self-center">Or</p>
        </div>

        <div class="d-flex flex-row"> 
            <button class="btn btn-outline-success mt-3 me-3 rounded-2 w-50 align-self-center">Sportify</button>
            <button class="btn btn-outline-dark mt-3 me-3 rounded-2 w-50 align-self-center">Google</button>
            <button class="btn btn-outline-primary mt-3 me-3 rounded-2 w-50 align-self-center">Facebook</button>
        </div>
    </form>
</div>

</template>

<script>
import SpotifySignInModal from './spotifySignInModal.vue';

export default{
    name: 'SignInForm',
    components: {
        SpotifySignInModal,
    },
    props: {
        error:{
            type: String,
            default: ''
        },
        isLoading: {
            type: Boolean,
            default: false
        },
        response: {
            type: Object,
            default: {}
        }
    },
    methods: {
        SignIn(){
            //e.preventDefault();
            //this.user
            this.$emit("toggleIsLoading","signIn", true);

            if(this.user.username.trim() === ""){
                this.$emit("setError","signIn", "Username cannot be empty");
                this.$emit("toggleIsLoading","signIn", false);
                return;
            }

            if(this.user.email.trim() === ""){
                this.$emit("setError","signIn", "Email cannot be empty");
                this.$emit("toggleIsLoading","signIn", false);
                return;
            }

            if(!this.doesPasswordMeetRequirements(this.pw)){
                this.$emit("setError","signIn", "Password does not meet the minimum requirements!");
                this.$emit("toggleIsLoading","signIn", false);
                return;
            }

            if(!this.validatePaswords(this.pw, this.confirmPw)){
                this.$emit("setError","signIn", "Confirm password does not match password!");
                this.$emit("toggleIsLoading","signIn", false);
                return;
            }

            this.$emit("registerUser", this.user);
            
        },
        validatePaswords(password, confirmPassword){

            if(password != confirmPassword || password == " " || confirmPassword == " " ){
                return false; 
            }

            this.user.password = password;

            return true;
        },
        doesPasswordMeetRequirements(password){

            // FIND A WAY TO GET A LIST OF COMMON PW'S
            const commonPasswords = [
              'password', 'password123', 'qwerty', '123456', 'letmein', 'admin', 'welcome'
            ];

            if (commonPasswords.includes(password.toLowerCase())){
                //console.log("hhhhh");
                return false;
            } 

            return this.strongPasswordRegex.test(password);
        },
        OpenModal(e){
            e.preventDefault();
            this.$refs.spotifySignInModal.openModal();
        },
        SetIntergrateAndSignIn(integrate){
            this.user.integrate = integrate

            console.log(this.user);
            this.SignIn();
        }
    },
    data(){
        return{
            user : {
                username: '',
                email: '',
                password: '',
                integrate: false
            },
            pw: '',
            confirmPw: '',
            strongPasswordRegex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[\]{}|;:'",.<>\/?`~])[A-Za-z\d!@#$%^&*()_\-+=\[\]{}|;:'",.<>\/?`~]{8,}$/
        }
    }
}

</script>

<style scoped>
.input-w{
    width : 85%;
}

</style>
