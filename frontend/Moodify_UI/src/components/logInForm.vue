<template>

<div class="py-5 border border-primary w-100 d-flex justify-content-center align-items-center">
    <form class="border d-flex flex-column p-5 h-75 w-75">
        <h1 class="text-primary align-self-center mb-5">Log In</h1>

        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input 
                type="text" 
                class="form-control p-2" 
                placeholder="Email"
                v-model="user.Email"
            />
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input 
                type="password" 
                class="form-control border-end-0 p-2" 
                placeholder="Password"
                v-model="user.Password"
            />
            <span class="input-group-text border-start-0 bg-transparent hover-pointer p-2">#</span>
        </div>
        <p class="text-danger">{{ error }}</p>

        <button 
            type="submit" 
            class="btn btn-outline-success rounded-2 w-50 align-self-center mt-4"
            @click="Login"
        >
            
            
            <div v-if="isLoading" id="spinner" class="text-center">
              <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
              </div>
            </div>
            <p v-else>Log In</p>
        </button>
        
        <div class="d-flex flex-row justify-content-center mt-5">
            <p class="align-self-center">Or</p>
        </div>

        <div class="d-flex flex-row"> 
            
            <button class="btn btn-outline-dark mt-3 me-3 rounded-2 w-50 align-self-center">Google</button>
            <button class="btn btn-outline-primary mt-3 me-3 rounded-2 w-50 align-self-center">Facebook</button>
            <button class="btn btn-outline-success mt-3 me-3 rounded-2 w-50 align-self-center" @click ="spotifyLogin">Sportify</button>
        </div>
    </form>
</div>

</template>

<script>

export default{
    name: 'LogInForm',
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
    data(){
        return{
            user: {
                Email : '',
                Password : '',
                UseSpotify: false
            }
        }
    },
    methods: {
        async Login(e){

            try{
                e.preventDefault();

                this.$emit("toggleIsLoading","logIn" ,true);

                if(this.user.Email.trim() === "" ){
                    this.$emit("setError","logIn", "Email cannot be empty");
                    this.$emit("toggleIsLoading","logIn", false);
                    return;
                }

                if(this.user.Password.trim() === ""){
                    this.$emit("setError","logIn", "Password cannot be empty");
                    this.$emit("toggleIsLoading","logIn", false);
                    return;
                }

                this.user.UseSpotify = false;
        
                this.$emit("loginUser", this.user);

            }
            catch(err){
                this.$emit("setError","logIn", err.message);
                this.$emit("toggleIsLoading","logIn", false);
                
            }
            
        },
        async spotifyLogin(e){
            try{
                e.preventDefault();
                this.user.UseSpotify = true;
                this.$emit("spotifyLogin", this.user);
                
            }
            catch(err){
                console.log(err);
            }
        }
    }
}

</script>

<style scoped>
.input-w{
    width : 85%;
}

</style>