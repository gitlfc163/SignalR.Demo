<template>
    <input type="text" v-model="state.userMessage" v-on:keypress="txtMsgOnkeypress" />
    <div>
        <ul>
            <li v-for="(msg, index) in state.messages" :key="index">{{ msg }}</li>
        </ul>
    </div>
</template>
  
  
<script>
import { reactive, onMounted } from "vue";
import * as signalR from "@microsoft/signalr";
let connection;
export default {
    name: "Login",
    setup() {
        const state = reactive({ userMessage: "", messages: [] });
        const txtMsgOnkeypress = async function (e) {
            if (e.keyCode != 13) return;
            await connection.invoke("SendPublicMessage", state.userMessage);
            state.userMessage = "";
        };
        onMounted(async function () {
            connection = new signalR.HubConnectionBuilder() // 创建从客户端到服务器端的连接
                .withUrl("https://localhost:5207/Hubs/ChatRoomHub") // 设置服务器端集线器的地址
                .withAutomaticReconnect() // 设置自动重连机制
                .build(); // 构建完成
            await connection.start(); // 启动
            // 通过on函数来注册监听服务器端使用SendAsync发送的消息的代码
            connection.on("ReceivePublicMessage", (msg) => {

                state.messages.push(msg);
            });
        });
        return { state, txtMsgOnkeypress };
    },
};
</script>