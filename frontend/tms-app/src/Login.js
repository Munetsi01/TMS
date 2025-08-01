import {useRef, useState, useEffect, useContext} from 'react';
import AuthContext from './context/AuthProvider';
import axios from './api/axios';

const LOGIN_URL = '/auth/login';

const Login = () => {

    const {setAuth} = useContext(AuthContext);
    const userRef = useRef();
    const errRef = useRef();

    const[user, setUser] = useState('');
    const[pwd, setPwd] = useState('');
    const[errMsg, setErrMsg] = useState('');
    const[success, setSuccess] = useState(false);

    useEffect(()=> {
        userRef.current.focus();
    },[])

      useEffect(()=> {
        setErrMsg('');
    },[user,pwd])

    const handleSubmit = async (e) => {
     e.preventDefault();

     try{

        const response = await axios.post(LOGIN_URL,JSON.stringify({Username:user,Password:pwd}), 
        {
           headers : {'Content-Type':'application/json', 
                      'Access-Control-Allow-Origin':'*',
                      'Access-Control-Allow-Methods':'GET, POST, PATCH, PUT, DELETE, OPTIONS',
                      'Access-Control-Allow-Headers':'Origin, Content-Type, X-Auth-Token'}
           //withCredentials:true
        });

    //console.log(JSON.stringify(response?.data));
    console.log(JSON.stringify(response));

    const accessToken = response?.data?.Token;
    setAuth({user,pwd,accessToken});

     setUser('');
     setPwd('');
     setSuccess(true);
     }
     catch(err){
       if(!err?.response){
        setErrMsg("No Server Response")
       }
       else if(err.response?.status === 400){
        setErrMsg("Missing Username or Password")
       }
       else{
            console.log(err);
        setErrMsg("Login Falied")
       }

       errRef.current.focus();
     }    
    }

    return(
       <>
       { success ? (
        <section>
            <h1>You are logged in!</h1>
            <br/>
            <p>
                <a href="#">Go to Home</a>
            </p>
        </section>
       ) : 
       ( <section>
         <p ref={errRef} className={errMsg ?"errmsg":"offscreen"} aria-live="assertive">{errMsg}</p>
         <h1>Sign In</h1>
         <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username:</label>
            <input type="text" 
                id="username"
                ref={userRef}
                autoComplete="off"
                onChange={(e) => setUser(e.target.value)}
                value ={user}
                required/>

                <input type="password" 
                id="password"
                onChange={(e) => setPwd(e.target.value)}
                value ={pwd}
                required/>

                <button>Sign In</button>

                <p>
                    Need an Account? <br/>
                    <span className="line">
                        {/*put router link here*/}
                        <a href="#">Sign Up</a>
                    </span>
                </p>
         </form>
        </section>)
        }
       </>
    )
}

export default Login