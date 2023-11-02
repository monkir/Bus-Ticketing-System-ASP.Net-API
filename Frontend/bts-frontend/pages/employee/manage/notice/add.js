import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import EmployeeHeader from '@/pages/employee/component/header';
import EmployeeFooter from '../../component/footer';
import Link from 'next/link';

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
                process.env.NEXT_PUBLIC_api_root + '/api/employee/notice/add',
                content,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setMessage("Bus Provider is added successfully")
            document.getElementById('my_modal_1').showModal();
            // setTimeout(() => { router.push('/employee/manage/notice') }, 2000);
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
                                {/* title name */}
                                <input type="text" placeholder="title" className="input input-bordered w-full max-w-xs"
                                    {...register("title", { required: { value: true, message: "title is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.title?.message}
                                    </span>
                                </label>
                                {/* description */}
                                <textarea type="text" placeholder="description" className="input input-bordered w-full max-w-xs"
                                    {...register("description", { required: { value: true, message: "description is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.description?.message}
                                    </span>
                                </label>
                            </div>
                            <div>
                                <span>{message}</span>
                            </div>

                            <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                                <Link className='btn btn-info' href={'/employee/manage/notice'}>Cancel</Link>
                                <input
                                    type="submit"
                                    value="Publish"
                                    className="btn btn-warning"
                                />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            {/* Modal */}
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/employee/manage/notice') }}>

                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">New notice is published successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/employee/manage/notice') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <EmployeeFooter />
        </>

    );
}