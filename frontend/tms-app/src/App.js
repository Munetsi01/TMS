import './App.css';
import Layout from './components/Layout';
import Login from './components/Login';
import Register from './components/Register';
import Home from './components/Home';
import Missing from './components/Missing';
import Unauthorized from './components/Unauthorized';
import RequireAuth from './components/RequireAuth';
import Dashboard from './components/Dashboard';
import {Route, Routes} from 'react-router-dom'

function App() {
  return (
    <div className="App">
     <Routes>

      <Route path='/' element={<Layout />}>
        {/*public routes*/}
        <Route path='/login' element={<Login/>}/>
        <Route path='/register' element={<Register/>}/>
        <Route path='/unauthorized' element={<Unauthorized/>}/>

        {/*protected routes*/}
        <Route element={<RequireAuth/>}>
        <Route path='/' element={<Home/>}/>
        <Route path='/dashboard' element={<Dashboard/>}/>
        </Route>

          {/*catch all*/}
        <Route path='*' element={<Missing/>}/>
      </Route>
      </Routes>
    </div>
  );
}

export default App;
