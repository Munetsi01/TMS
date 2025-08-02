import {useRef, useState, useEffect, useContext} from 'react';
import {Link} from 'react-router-dom'
import AuthContext from './context/AuthProvider';
import axios from './api/axios';
//import {Link} from 'react-router-dom'

const LOGIN_URL = '/auth/register';

const Register = () => {

    const {setAuth} = useContext(AuthContext);
    const userRef = useRef();
    const errRef = useRef();

    const[user, setUser] = useState('');

    const[email, setEmail] = useState('');

    const[password, setPassword] = useState('');

    const[confirmPassword, setConfirmPassword] = useState('');

    const[errMsg, setErrMsg] = useState('');
    const[success, setSuccess] = useState(false);

    useEffect(()=> {
        userRef.current.focus();
    },[])


      useEffect(()=> {
        setErrMsg('');
    },[user, email, password, confirmPassword])

    const handleSubmit = async (e) => {
     e.preventDefault();

     try{
          //validate
          if (password != confirmPassword) {
            setErrMsg("Password and Confirm Password don't match");
            return;
        }

        const response = await axios.post(LOGIN_URL,JSON.stringify({username:user, email:email, password:password}), 
        {
           headers : {'Content-Type':'application/json'}
        });

    console.log(JSON.stringify(response?.data));

    const accessToken = response?.data?.Token;
    setAuth({user,password,accessToken});

     setUser('');
     setPassword('');
     setEmail('');
     setConfirmPassword('')

     setSuccess(true);
     }
     catch(err){
       if(!err?.response){
        setErrMsg("No Server Response")
       }
       else if(err.response?.status === 400){
        setErrMsg(err?.response?.data?.message);
       }
       else{
            console.log(err?.response?.data);
        setErrMsg("Login Falied")
       }
       errRef.current.focus();
     }    
    }

    return(
       <>
       { success ? (
        <section>
            <h1>You have successfully registered!</h1>
            <br/>
            <p>
             <Link to="/">Login</Link>
            </p>
        </section>
       ) : 
       ( <section>
         <p ref={errRef} className={errMsg ?"errmsg":"offscreen"} aria-live="assertive">{errMsg}</p>
         <h1>Register</h1>
         <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username:</label>
            <input type="text" 
                id="username"
                ref={userRef}
                autoComplete="off"
                onChange={(e) => setUser(e.target.value)}
                value ={user}
                required/>

            <label htmlFor="email">Email:</label>
            <input type="email" 
                id="email"
                ref={userRef}
                autoComplete="off"
                onChange={(e) => setEmail(e.target.value)}
                value ={email}
                required/>

            <label htmlFor="password">Password:</label>
                <input type="password" 
                id="password"
                onChange={(e) => setPassword(e.target.value)}
                value ={password}
                required/>

            <label htmlFor="confirmPassword">Confirm Password:</label>
                <input type="password" 
                id="confirmPassword"
                onChange={(e) => setConfirmPassword(e.target.value)}
                value ={confirmPassword}
                required/>
            
                <button>Register</button>

                <p>
                    Have an Account? <br/>
                    <span className="line">
                        <Link to="/">Login</Link>
                    </span>
                </p>
         </form>
        </section>)
        }
       </>
    )
}

export default Register