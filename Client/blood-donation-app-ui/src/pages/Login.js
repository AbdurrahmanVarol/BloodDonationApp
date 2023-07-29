import React from 'react'
import LoginForm from '../components/LoginForm'

const Login = () => {
  return (
    <div style={{width:"100%"}}>
      <h1 class="text-center text-dark-emphasis mb-4">Giriş Yap</h1>
        <LoginForm></LoginForm>
    </div>
  )
}

export default Login