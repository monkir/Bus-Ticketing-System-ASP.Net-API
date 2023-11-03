import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import BusProviderHeader from '@/pages/busProvider/component/header';
import BusProviderFooter from '@/pages/busProvider/component/footer';
import Link from 'next/link';

export default function App() {
    const [message, setMessage] = useState('');
    const router = useRouter();
    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async data => {
        let ammount = data['ammount']
        let content = {}
        for (const key in data) {
            content[key] = data[key]
        }
        try {
            const response = await axios.post(
                process.env.NEXT_PUBLIC_api_root + '/api/user/account/withdraw/' + ammount,
                content,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setMessage(response.data.message)
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

            <BusProviderHeader title="Bus Ticketing System" pagename="Bus provider: Deposit money"></BusProviderHeader>
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <img
                            className="mx-auto h-10 w-auto"
                            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                            alt="Your Company"
                        />
                        <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                            Deposit Money
                        </h2>
                    </div>
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">


                        <form onSubmit={handleSubmit(onSubmit)} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
                            <div className="form-control w-full max-w-xs">
                                {/* ammount name */}
                                <input type="number" placeholder="ammount" className="input input-bordered w-full max-w-xs"
                                    {...register("ammount", {
                                        required: { value: true, message: "Ammount is required" },
                                        min: { value: 500, message: "Minimum ammount is 500" }
                                    })}
                                />
                                <label className="label">
                                    <span className="label-text-alt text-red-500 text-xs italic">
                                        {errors.ammount?.message}
                                    </span>
                                </label>
                            </div>
                            <div>
                                <span>{message}</span>
                            </div>
                            <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                                <Link className='btn btn-info' href={'/busProvider/account'}>Cancel</Link>
                                <input
                                    type="submit"
                                    value="Edit"
                                    className="btn btn-warning"
                                />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            {/* Modal */}
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/busProvider/account') }}>

                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Amount is withdrawn successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/busProvider/account') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <BusProviderFooter />
        </>

    );
}