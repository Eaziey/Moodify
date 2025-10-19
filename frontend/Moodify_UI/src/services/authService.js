import axios from "axios";

const Api_Base_URL = 'https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5063.app.github.dev';

export default{

    async registerUserAsync(UserInfo) {

        try{
            var response = await axios.post(`${Api_Base_URL}/api/Auth/signIn`,UserInfo, {
            withCredentials: true
            });


            if(response && response.status == 201){

                router.push('/home');
            }
        }
        catch(err){
            console.log(`an error has occurred: ${err}`);
        }
       
        
    },

    async registerUserWithSpotifyLoginAsync(UserInfo) {

        try{
            var response = await axios.post(`${Api_Base_URL}/api/Auth/signIn`,UserInfo, {
                withCredentials: true
            });

            if (!this.response.data.redirectUrl) {
                throw new Error("redirect URL was not returned.")       
            }

            // Spotify integration: redirect to Spotify authorization
            window.location.href = this.response.data.redirectUrl;
            return;
        }
        catch(err){
            console.log(`an error has occurred: ${err}`);
        }
    },

    async loginUserAsync(UserInfo){
        try{
            var response = await axios.post(`${Api_Base_URL}/api/Auth/logIn`,UserInfo);

            if(response && response.status == 201){

                router.push('/home');
            }
        }
        catch(err){
            console.log(`an error has occurred: ${err}`);
        }
    },

    async spotifyLogin(UserInfo){
        try{
            
            var response = await axios.post(`${Api_Base_URL}/api/Auth/logIn`,UserInfo, {
                withCredentials: true
            });

            if (!this.response.data.redirectUrl) {
                throw new Error("redirect URL was not returned.")       
            }

            // Spotify integration: redirect to Spotify authorization
            window.location.href = this.response.data.redirectUrl;
            return;


        }
         catch(err){
            console.log(`an error has occurred: ${err}`);
        }
    },

    async logout(){
        try{
            var response = await axios.post(`${Api_Base_URL}/api/Auth/logout`, {
                withCredentials: true
            });

            if(response && response.status === 200){

                console.log(response.data.message);
            }
        }
        catch(err){
            console.log(`an error has occurred: ${err}`);
        }
    }
}