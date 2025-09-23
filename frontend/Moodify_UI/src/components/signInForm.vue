<template>

<div class="py-5 border border-primary w-100 d-flex justify-content-center align-items-center">
    <form class="border d-flex flex-column p-5 h-75 w-75">
        <h1 class="text-primary align-self-center mb-5">Register</h1>

        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input type="text" class="form-control p-2" placeholder="Username"/>
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input type="email" class="form-control p-2" placeholder="Email"/>
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input type="password" class="form-control border-end-0 p-2" placeholder="Password"/>
            <span class="input-group-text border-start-0 bg-transparent hover-pointer p-2">#</span>
        </div>
        <div class="input-group align-self-center my-3 input-w">
            <span class="input-group-text p-2">@</span>
            <input type="password" class=" form-control border-end-0 p-2" placeholder="Confirm Password"/>
            <span class="input-group-text border-start-0 bg-transparent hover-pointer p-2">#</span>
        </div>

        <button 
            type="submit" 
            class="btn btn-outline-success rounded-2 w-50 align-self-center mt-4"
            @click = "SignIn">
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
            this.$router.push("/home");
        }
    }
}

</script>

<style scoped>
.input-w{
    width : 85%;
}

</style>
