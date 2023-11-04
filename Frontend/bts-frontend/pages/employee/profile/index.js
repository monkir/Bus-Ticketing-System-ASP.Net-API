import React, { useEffect } from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import { useRouter } from 'next/router';
import Link from 'next/link';
import EmployeeHeader from '../component/header';
import EmployeeFooter from '../component/footer';

export default function App() {
    const [message, setMessage] = useState('');
    const router = useRouter();
    const [data, setData] = useState();
    async function fetchData() {
        try {
            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/user/profile',
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setData(response.data)
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
        fetchData();
    }, [])

    return (
        <>
            <EmployeeHeader title="Bus Ticketing System" pagename="Employee: My Profile"/>
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <img
                            className="mx-auto h-10 w-auto"
                            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                            alt="Your Company"
                        />
                        <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                            My Profile
                        </h2>
                    </div>
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <div className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full'>
                            {
                                data == null
                                    ? <div className="w-full max-w-xs">
                                        {/* Loading */}
                                        <div className='w-full text-xl ml-2 my-2 max-w-xs'>
                                            <p class="text-lg font-medium text-gray-900 dark:text-white">
                                                <span className='txt text-blue-500'>
                                                    Data is loading
                                                </span>
                                            </p>
                                        </div>
                                    </div>
                                    : <div className="w-full max-w-xs">
                                        {/* id */}
                                        <div className='w-full text-xl ml-2 my-2 max-w-xs'>
                                            <p class="text-lg font-medium text-gray-900 dark:text-white">
                                                ID: <span className='txt text-blue-500'>{data.id}</span>
                                            </p>
                                        </div>
                                        {/* username */}
                                        <div className='w-full text-xl ml-2 my-2 max-w-xs'>
                                            <p class="text-lg font-medium text-gray-900 dark:text-white">
                                                Name: <span className='txt text-blue-500'>{data.username}</span>
                                            </p>
                                        </div>
                                        {/* userRole */}
                                        <div className='w-full text-xl ml-2 my-2 max-w-xs'>
                                            <p class="text-lg font-medium text-gray-900 dark:text-white">
                                                User Role: <span className='txt text-blue-500'>{data.userRole}</span>
                                            </p>
                                        </div>
                                    </div>
                            }
                            <div>
                                <span>{message}</span>
                            </div>
                            <div class="grid grid-flow-col justify-stretch space-x-2 mx-2">
                                <Link className='btn btn-info' href={'/employee'}>Back to Panel</Link>
                                <Link className='btn btn-warning' href={'/employee/profile/changepassword'}>Change Password</Link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <EmployeeFooter />
        </>

    );
}