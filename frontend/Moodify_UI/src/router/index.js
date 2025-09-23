import LoginRegister from "../pages/loginRegister.vue";
import Home from "../pages/home.vue";

import { createWebHistory, createRouter } from 'vue-router'


const routes = [
    {path: "/", component: LoginRegister},
    {path: "/home", component: Home}
]

const router = createRouter ({
    history: createWebHistory(),
    routes
})

export default router;