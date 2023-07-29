import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import { DefaultContextProvider } from './contexts/DefaultContext';
import AuthLayout from './layouts/AuthLayout';
import DefaultLayout from './layouts/DefaultLayout';
import Login from './pages/Login';
import { useEffect } from 'react';
import Register from './pages/Register';
import Home from './pages/Home';

function App() {
  const getData = () => {
    fetch("/a/a/d")
    .then(response=>response.json())
    .then(data=>{})
    .catch(error=>console.log(error))
}
useEffect(()=>{
getData()
},[])
  return (
    <div>
      
      <DefaultContextProvider>
        <BrowserRouter>
          <Routes>
            <Route element={<AuthLayout/>}>
            <Route index path='/login' element={<Login/>}></Route>
            <Route path='/register' element={<Register/>}></Route>
            </Route>
            <Route element={<DefaultLayout/>}>
            <Route path='/' element={<Home/>}></Route>
            </Route>
          </Routes>
        </BrowserRouter>
      </DefaultContextProvider>


    </div>
  );
}

export default App;
