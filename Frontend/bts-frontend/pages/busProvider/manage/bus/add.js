import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import BusProviderHeader from '../../component/header';
import BusProviderFooter from '../../component/footer';

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
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/bus/add',
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
            <BusProviderHeader title="Bus Ticketing System" pagename="Employee: Manage Bus Provider"></BusProviderHeader>
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
                                {/* brand */}
                                <input type="text" placeholder="brand" className="input input-bordered w-full max-w-xs"
                                    {...register("brand", { required: { value: true, message: "brand is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.brand?.message}
                                    </span>
                                </label>
                                {/* model */}
                                <input type="text" placeholder="model" className="input input-bordered w-full max-w-xs"
                                    {...register("model", { required: { value: true, message: "model is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.model?.message}
                                    </span>
                                </label>
                                {/* serialNo */}
                                <input type="text" placeholder="serialNo" className="input input-bordered w-full max-w-xs"
                                    {...register("serialNo", { required: { value: true, message: "serialNo is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.serialNo?.message}
                                    </span>
                                </label>
                                {/* category */}
                                <select defaultValue={''} {...register("category", { required: { value: true, message: "category is required" } })}>
                                    <option value={''} disabled>Select Category</option>
                                    <option value={'AC'}>AC</option>
                                    <option value={'Non-AC'}>Non-AC</option>
                                </select>
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.category?.message}
                                    </span>
                                </label>
                                {/* totalSeat */}
                                <input type="number" placeholder="totalSeat" defaultValue={40} disabled className="input input-bordered w-full max-w-xs"
                                    {...register("totalSeat",{value: 40}, { required: { value: true, message: "totalSeat is required" } })} />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.totalSeat?.message}
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
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/busProvider/manage/bus') }}>
                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Bus is added successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/busProvider/manage/bus') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <BusProviderFooter />
        </>

    );
}