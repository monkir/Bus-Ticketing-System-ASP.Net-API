import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import AdminHeader from '@/pages/admin/component/header';
import AdminFooter from '@/pages/admin/component/footer';

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
                process.env.NEXT_PUBLIC_api_root + '/api/admin/employee/add',
                content,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setMessage("Employee is added successfully")
            document.getElementById('my_modal_1').showModal();
            // setTimeout(() => { router.push('/employee/manage/busprovider') }, 2000);
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

            <AdminHeader title="Bus Ticketing System" pagename="Employee: Manage Bus Provider"></AdminHeader>
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
                                {/* name */}
                                <input type="text" placeholder="name" className="input input-bordered w-full max-w-xs"
                                    {...register("name", { required: { value: true, message: "Name is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.name?.message}
                                    </span>
                                </label>
                                {/* salary */}
                                <input type="number" placeholder="salary" className="input input-bordered w-full max-w-xs"
                                    {...register("salary", { required: { value: true, message: "Salary is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.salary?.message}
                                    </span>
                                </label>
                                {/* dob */}
                                <input type="date" placeholder="dob" className="input input-bordered w-full max-w-xs"
                                    {...register("dob", { required: { value: true, message: "DOB is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.dob?.message}
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
            <dialog id="my_modal_1" className="modal" onClose={()=>{router.push('/admin/manage/employee')}}>
                
                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Bus provider is added successfully</p>
                    <div className="modal-action">
                        <button onClick={()=>{router.push('/busprovider/manage/admin')}} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <AdminFooter />
        </>

    );
}