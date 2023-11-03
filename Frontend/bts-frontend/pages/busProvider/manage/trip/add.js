import React, { useEffect } from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import BusProviderHeader from '../../component/header';
import BusProviderFooter from '../../component/footer';
import moment from 'moment/moment';

export default function App() {
    const [message, setMessage] = useState('');
    const [placesData, setPlacesData] = useState();
    const [busData, setBusData] = useState();
    const router = useRouter();
    const { register, handleSubmit, formState: { errors } } = useForm();

    async function fetchPlacesData() {
        try {

            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/trip/places',
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setPlacesData(response.data);
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

    async function fetchBusData() {
        try {

            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/bus/all',
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setBusData(response.data);
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

    useEffect(() => {
        if(placesData == null) {
            fetchPlacesData()
        }
        if(busData == null) {
            fetchBusData()
        }
    }, [placesData, busData])

    const onSubmit = async data => {
        console.log(data);
        let content = {}
        for (const key in data) {
            content[key] = data[key]
        }
        try {
            const response = await axios.post(
                process.env.NEXT_PUBLIC_api_root + '/api/busprovider/trip/add',
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
    const currentDateTime = ()=>{
        return moment().format(moment.HTML5_FMT.DATETIME_LOCAL)
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
                        {placesData == null || busData == null
                            ? <div className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'></div>
                            :
                            <form onSubmit={handleSubmit(onSubmit)} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
                                <div className="form-control w-full max-w-xs">
                                    {/* depot_id */}
                                    <select defaultValue={''} {...register("depot_id", { required: { value: true, message: "depot_id is required" } })}>
                                        <option value={''} disabled>Select Depot</option>
                                        {placesData.map(item=>(
                                            <option key={item.id} value={item.id}>{item.id+ ": "+ item.name}</option>
                                        ))}
                                    </select>
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.depot_id?.message}
                                        </span>
                                    </label>
                                    {/* dest_id */}
                                    <select defaultValue={''} {...register("dest_id", { required: { value: true, message: "dest_id is required" } })}>
                                        <option value={''} disabled>Select Destination</option>
                                        {placesData.map(item=>(
                                            <option key={item.id} value={item.id}>{item.id+ ": "+ item.name}</option>
                                        ))}
                                    </select>
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.dest_id?.message}
                                        </span>
                                    </label>
                                    {/* bus_id */}
                                    <select defaultValue={''} {...register("bus_id", { required: { value: true, message: "bus_id is required" } })}>
                                        <option value={''} disabled>Select Bus</option>
                                        {busData.map(item=>(
                                            <option key={item.id} value={item.id}>{item.id+ ": "+ item.brand+ ": "+item.model}</option>
                                        ))}
                                    </select>
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.bus_id?.message}
                                        </span>
                                    </label>
                                    {/* startTime */}
                                    <input type="datetime-local" defaultValue={currentDateTime()} placeholder="startTime" className="input input-bordered w-full max-w-xs"
                                        {...register("startTime", { required: { value: true, message: "startTime is required" } })} />
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.startTime?.message}
                                        </span>
                                    </label>
                                    {/* endTime */}
                                    <input type="datetime-local" defaultValue={currentDateTime()} placeholder="endTime" className="input input-bordered w-full max-w-xs"
                                        {...register("endTime", { required: { value: true, message: "endTime is required" } })} />
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.endTime?.message}
                                        </span>
                                    </label>
                                    {/* ticketPrice */}
                                    <input type="number" placeholder="ticketPrice" className="input input-bordered w-full max-w-xs"
                                        {...register("ticketPrice", 
                                        { 
                                            required: { value: true, message: "ticketPrice is required" } ,
                                            min: {value: 100, message: "Minimum ticket price is 100"}
                                        })} />
                                    <label className="label">
                                        <span className="label-text-alt text-red-500 text-xs italic">
                                            {errors.ticketPrice?.message}
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
                        }
                    </div>
                </div>
            </div>
            {/* Modal */}
            <dialog id="my_modal_1" className="modal" onClose={() => { router.push('/busProvider/manage/trip') }}>
                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">trip is added successfully</p>
                    <div className="modal-action">
                        <button onClick={() => { router.push('/busProvider/manage/trip') }} className="btn">Ok</button>
                    </div>
                </div>
            </dialog>
            <BusProviderFooter />
        </>

    );
}