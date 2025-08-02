//import {useRef, useState, useEffect, useContext} from 'react';
import axios from '../api/axios';

const TASKS_URL = '/tasks';

const Home = () => {

    try{
        //     const response = axios.get(TASKS_URL, 
        //     {
        //        headers : {'Content-Type':'application/json'}
        //     });
    
        // console.log(JSON.stringify(response?.data));
        //success
        // const accessToken = response?.data?.Token;
        // setAuth({user,pwd,accessToken});
    
        //  setUser('');
        //  setPwd('');
        //  setSuccess(true);
         }
         catch(err){
           if(!err?.response){
            //setErrMsg("No Server Response")
           }
           else if(err.response?.status === 400){
                    //setErrMsg(err?.response?.data?.message);
           }
            else if(err.response?.status === 401){
                   // setErrMsg(err?.response?.data?.message);
           }
            else if(err.response?.status === 403){
                    //setErrMsg(err?.response?.data?.message);
           }
           else{
                console.log(err?.response?.data);
            //setErrMsg("Tasks Loading Falied")
           }
        }
    return(
     <div>
        Home is under construction
        Loading Tasks from background
     </div>
    )
     
    }
export default Home