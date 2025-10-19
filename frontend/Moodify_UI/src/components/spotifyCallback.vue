<template>

<div>
    <p v-if='error == ""'>Logging in with Spotify...</p>
    <p v-else >{{ error }}</p>
</div>

</template>

<script>   

export default{
    name: 'SpotifyCallback',
    methods:{
        async mount(){

            const urlParams = new URLSearchParams(window.location.search);
            const code = urlParams.get('code');
            const state = urlParams.get('state');

            try{
                const response = await axios.get(`https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5063.app.github.dev/api/Auth/spotify-callback?code=${code}&state=${state}`,{
                    withCredentials: true
                });

                //const token = response.data.Token;
                //localStorage.setItem('Jwt_Token', token);
                if(response && response.status == 201){

                    router.push('/home');
                }
                //window.location.href = '/home';
            }
            catch(err){
                console.error("Spotify login failed:", err);
                this.error = "Spotify login failed: " + err;
            }
        }
    },
    data(){
        return{
            error: ""
        }
    }
}
</script>