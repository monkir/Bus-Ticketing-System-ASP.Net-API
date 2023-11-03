import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import Link from 'next/link';
import MyHeader from './component/header';
import MyFooter from './component/footer';
export default function App() {
  const [message, setMessage] = useState('');
  const [userRole, setUserRole] = useState();
  const router = useRouter();
  const { register, handleSubmit, formState: { errors } } = useForm();
  const onSubmit = async data => {
    console.log(data);
    let content = {}
    for (const key in data) {
      content[key] = data[key]
    }
    try {
      const response = await axios.post(
        process.env.NEXT_PUBLIC_api_root + '/api/user/login',
        content
      )
      sessionStorage.setItem("userrole", response.data.userrole)
      sessionStorage.setItem("token_string", response.data.token.token_string)
      setMessage("Login is successfull")
      setUserRole(response.data.userrole)
      document.getElementById('my_modal_1').showModal();
      // setTimeout(() => { router.push('/' + response.data.userrole) }, 2000);
    }
    catch (e) {
      try {
        console.log(e)
        setMessage(e.response.data.Message)
      }
      catch {
        setMessage("API is not connected")
      }
    }
  }

  return (
    <>
      <MyHeader title="Bus Ticketing System" pagename="Employee: Manage Bus Provider" />
      <div className="overflow-x-auto px-10 min-h-[70vh]">
        <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
          <div className="sm:mx-auto sm:w-full sm:max-w-sm">
            <img
              className="mx-auto h-10 w-auto"
              src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
              alt="Your Company"
            />
            <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
               Login Here
            </h2>
          </div>
          <div className="sm:mx-auto sm:w-full sm:max-w-sm">
            <form onSubmit={handleSubmit(onSubmit)} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
              <div className="form-control w-full max-w-xs">
                {/* username */}
                <input type="text" placeholder="username" className="input input-bordered w-full max-w-xs"
                  {...register("username", { required: { value: true, message: "username is required" } })} />
                <label className="label">
                  <span className="label-text-alt text-red-500 text-xs italic">
                    {errors.username?.message}
                  </span>
                </label>
                {/* password */}
                <input type="text" placeholder="password" className="input input-bordered w-full max-w-xs"
                  {...register("password", { required: { value: true, message: "password is required" } })} />
                <label className="label">
                  <span className="label-text-alt text-red-500 text-xs italic">
                    {errors.password?.message}
                  </span>
                </label>
              </div>
              <div>
                <span>{message}</span>
              </div>
              <Link href="/forgot-password" className="font-semibold leading-4 text-red-600 hover:text-indigo-500">
                Forgot password
              </Link>
              <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                <Link className='btn btn-info' href={'/'}>Cancel</Link>
                <input
                  type="submit"
                  value="Login"
                  className="btn btn-warning"
                />
              </div>
              <p className="mt-10 text-center text-sm text-gray-500">
                Not a member?{' '}
                <Link href="/registration" className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500">
                  Free registration
                </Link>
              </p>
            </form>
          </div>
        </div>
      </div>
      {/* Modal */}
      <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/' + userRole) }}>
        <div className="modal-box">
          <h3 className="font-bold text-lg">Hello!</h3>
          <p className="py-4">{message}</p>
          <div className="modal-action">
            <button onClick={() => { router.push('/' + userRole) }} className="btn">Ok</button>
          </div>
        </div>
      </dialog>
      <MyFooter />
    </>

  );
}