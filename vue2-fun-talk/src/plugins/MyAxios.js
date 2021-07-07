import axios from "axios";

let token = localStorage.getItem("funtalk");

let myAxios = axios.create({
  headers: { authorization: `bearer ${token}` },
});

export default myAxios;
