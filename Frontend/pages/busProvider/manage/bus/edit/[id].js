import React, { useEffect } from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import BusProviderHeader from '@/pages/busProvider/component/header';
import BusProviderFooter from '@/pages/busProvider/component/footer';
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
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/bus/get/' + id,
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
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/bus/update',
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

            <BusProviderHeader title="Bus Ticketing System" pagename="Bus Provider: Edit Bus"></BusProviderHeader>
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <img
                            className="mx-auto h-10 w-auto"
                            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                            alt="Your Company"
                        />
                        <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                            Edit Bus
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
                                            {/* brand */}
                                            <input type="text" placeholder="brand" defaultValue={data.brand} className="input input-bordered w-full max-w-xs"
                                                {...register("brand", { required: { value: true, message: "brand is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.brand?.message}
                                                </span>
                                            </label>
                                            {/* model */}
                                            <input type="text" placeholder="model" defaultValue={data.model} className="input input-bordered w-full max-w-xs"
                                                {...register("model", { required: { value: true, message: "model is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.model?.message}
                                                </span>
                                            </label>
                                            {/* serialNo */}
                                            <input type="text" placeholder="serialNo" defaultValue={data.serialNo} className="input input-bordered w-full max-w-xs"
                                                {...register("serialNo", { required: { value: true, message: "serialNo is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.serialNo?.message}
                                                </span>
                                            </label>
                                            {/* category */}
                                            <select defaultValue={data.category} {...register("category", { required: { value: true, message: "category is required" } })}>
                                                <option value={''} selected disabled>Select Category</option>
                                                <option value={'AC'}>AC</option>
                                                <option value={'Non-AC'}>Non-AC</option>
                                            </select>
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.category?.message}
                                                </span>
                                            </label>
                                            {/* totalSeat */}
                                            <input type="number" placeholder="totalSeat" defaultValue={data.totalSeat} disabled className="input input-bordered w-full max-w-xs"
                                                {...register("totalSeat", { value: 40 }, { required: { value: true, message: "totalSeat is required" } })} />
                                            <label className="label">
                                                <span className="label-text-alt text-red-500 text-xs italic">
                                                    {errors.totalSeat?.message}
                                                </span>
                                            </label>

                                        </div>
                                        <div>
                                            <span className="label-text-alt text-red-500 text-lg italic">
                                                {message}
                                            </span>
                                        </div>
                                        <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                                            <Link className='btn btn-info' href={'/busProvider/manage/bus'}>Cancel</Link>
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
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/busProvider/manage/bus') }}>

                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Bus is updated successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/busProvider/manage/bus') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <BusProviderFooter />
        </>

    );
}