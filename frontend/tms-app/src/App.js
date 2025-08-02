import './App.css';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom'
import Login from './Login';
import Register from './Register';
import Home from './Home';

function App() {
  return (
    <div className="App">
     <Router>
      <Routes>
        <Route path='/' element={<Login/>}/>
        <Route path='/login' element={<Login/>}/>
        <Route path='/register' element={<Register/>}/>
        <Route path='/home' element={<Home/>}/>
      </Routes>
     </Router>
    </div>
  );
}

export default App;
