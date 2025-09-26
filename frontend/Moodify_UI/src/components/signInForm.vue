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

        <button 
            type="submit" 
            class="btn btn-outline-success rounded-2 w-50 align-self-center mt-4"
            @click="SignIn">
                Sign In
        </button>
        
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
export default{
    name: 'SignInForm',
    methods: {
        SignIn(e){
            e.preventDefault();


            if(this.user.username.trim() === "" || this.user.email.trim() === ""){
                console.log("Username or Email cannot be empty");
                return;
            }

            if(!this.doesPasswordMeetRequirements(this.pw)){
                console.log("Password does not meet the minimum requirements!");
                return;
            }

            if(!this.validatePaswords(this.pw, this.confirmPw)){
                console.log("Confirm password does not match password!");
                return;
            }

            this.$emit("AuthUser", this.user);


            this.$router.push("/home");
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
        }
    },
    data(){
        return{
            user : {
                username: '',
                email: '',
                password: ''
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
