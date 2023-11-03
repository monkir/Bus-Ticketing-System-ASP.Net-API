import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import EmployeeHeader from '@/pages/employee/component/header';
import EmployeeFooter from '../../component/footer';

export default function App() {
    const [message, setMessage] = useState('');
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
                process.env.NEXT_PUBLIC_api_root + '/api/employee/busprovider/add',
                content,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setMessage("Bus Provider is added successfully")
            document.getElementById('my_modal_1').showModal();
            // setTimeout(() => { router.push('/employee/manage/busprovider') }, 2000);
        }
        catch (e) {
            try {
                console.log(e)
                setMessage(e.response.data.message)
            }
            catch {
                setMessage("API is not connected")
            }
        }
    }

    return (
        <>

            <EmployeeHeader title="Bus Ticketing System" pagename="Employee: Manage Bus Provider"></EmployeeHeader>
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <img
                            className="mx-auto h-10 w-auto"
                            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                            alt="Your Company"
                        />
                        <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                            Add Bus Provider
                        </h2>
                    </div>
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">


                        <form onSubmit={handleSubmit(onSubmit)} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
                            <div className="form-control w-full max-w-xs">
                                {/* Company name */}
                                <div className='mb-4'>

                                </div>
                                <input type="text" placeholder="company" className="input input-bordered w-full max-w-xs"
                                    {...register("company", { required: { value: true, message: "Company name is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.company?.message}
                                    </span>
                                </label>
                                {/* User name */}
                                <input type="text" placeholder="username" className="input input-bordered w-full max-w-xs"
                                    {...register("username", { required: { value: true, message: "User name is required" } })} />
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
                            <input 
                            type="submit"
                            className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                            />
                        </form>
                    </div>
                </div>
            </div>
            {/* Modal */}
            <dialog id="my_modal_1" className="modal" onClose={()=>{router.push('/employee/manage/busprovider')}}>
                
                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Bus provider is added successfully</p>
                    <div className="modal-action">
                        <button onClick={()=>{router.push('/employee/manage/busprovider')}} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <EmployeeFooter />
        </>

    );
}