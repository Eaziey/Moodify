import axios from "axios";

const Api_Base_URL = 'https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5063.app.github.dev';

export default{

    registerUserAsync(UserInfo) {

        return axios.post(`${Api_Base_URL}/api/Auth/signIn`,UserInfo);
    },

    loginUserAsync(UserInfo){
         return axios.post(`${Api_Base_URL}/api/Auth/logIn`,UserInfo);
    }
}