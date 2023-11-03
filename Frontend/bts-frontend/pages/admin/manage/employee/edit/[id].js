import React, { useEffect } from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import AdminHeader from '@/pages/admin/component/header';
import AdminFooter from '@/pages/admin/component/footer';
import Link from 'next/link';

export default function App() {
    const [message, setMessage] = useState('');
    const [data, setData] = useState();
    const router = useRouter();
    const { register, handleSubmit, formState: { errors } } = useForm();
    const { id } = router.query
    async function fetchData() {
        try {
            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/admin/employee/get/' + id,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setData(response.data)
            console.log(data)
        }
        catch (e) {
            try {
                console.log(e)
                setMessage(e.response.data.Message)
            }
            catch {
                console.log(e)
                setMessage("API is not connected")
            }
        }
    }
    const onSubmit = async data => {
        // console.log(data);
        let content = {}
        for (const key in data) {
            content[key] = data[key]
        }
        try {
            const response = await axios.put(
                process.env.NEXT_PUBLIC_api_root + '/api/admin/employee/update',
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
    useEffect(() => {
        if (id !== undefined) {
            fetchData()
        }
    }, [id])

    const dateToShortDate = (date) => {
        const d = new Date(date);
        return d.toISOString().split('T')[0];
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
                        {
                            data == null
                                ? <span>From is loading</span>
                                :
                                <>
                                    <form onSubmit={handleSubmit(onSubmit)} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
                                        <div className="form-control w-full max-w-xs">
                                            {/* ID */}
                                            <input type="hidden" placeholder="id" value={data.id} {...register("id", { required: { value: true, message: "ID is required" } })} />
                                            {/* name */}
                                            <input type="text" placeholder="name" defaultValue={data.name} className="input input-bordered w-full max-w-xs"
                                                {...register("name", { required: { value: true, message: "Name is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.name?.message}
                                                </span>
                                            </label>
                                            {/* salary */}
                                            <input type="number" placeholder="salary" defaultValue={data.salary} className="input input-bordered w-full max-w-xs"
                                                {...register("salary", { required: { value: true, message: "Salary is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.salary?.message}
                                                </span>
                                            </label>
                                            {/* dob */}
                                            <input type="date" placeholder="dob" defaultValue={dateToShortDate(data.dob)} className="input input-bordered w-full max-w-xs"
                                                {...register("dob", { required: { value: true, message: "DOB is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.dob?.message}
                                                </span>
                                            </label>
                                            {/* User name */}
                                            <input type="text" placeholder="username" defaultValue={data.username} className="input input-bordered w-full max-w-xs"
                                                {...register("username", { required: { value: true, message: "User name is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.username?.message}
                                                </span>
                                            </label>
                                            {/* password */}
                                            {/* <input type="text" placeholder="password" defaultValue={data.password} className="input input-bordered w-full max-w-xs"
                                                {...register("password", { required: { value: true, message: "password is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.password?.message}
                                                </span>
                                            </label> */}
                                        </div>
                                        <div>
                                            <span className="label-text-alt text-red-500 text-lg italic">
                                                {message}
                                            </span>
                                        </div>
                                        <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                                            <Link className='btn btn-info' href={'/admin/manage/employee'}>Cancel</Link>
                                            <input
                                                type="submit"
                                                value="Edit"
                                                className="btn btn-warning"
                                            />
                                        </div>
                                    </form>
                                </>
                        }

                    </div>
                </div>
            </div>
            {/* Modal */}
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/admin/manage/employee') }}>

                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Employee is updated successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/admin/manage/employee') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <AdminFooter />
        </>

    );
}