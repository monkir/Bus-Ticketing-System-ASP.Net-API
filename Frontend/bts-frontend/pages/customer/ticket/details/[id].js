import axios from "axios";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import CustomerHeader from "../../component/header";
import CustomerFooter from "../../component/footer";
import { useForm } from "react-hook-form";

export default function BookSeat() {
    const router = useRouter()
    const numbers = Array.from({ length: 40 }, (value, index) => index + 1);
    const [data, setData] = useState()
    const [ticketData, setTicketData] = useState();
    const [bookedSeat, setBookedSeat] = useState([]);
    const [ticketSeat, setTicketSeat] = useState([]);
    const [tripId, setTripId] = useState();
    const [message, setMessage] = useState("")
    const { id } = router.query
    async function fetchTicketData() {
        // Fetching ticket Data
        try {
            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/customer/ticket/get/' + id,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setTicketData(response.data)
            setTicketSeat(response.data.seat_no)
            setTripId(response.data.trip_id)
            console.log(data)
            // console.log(response.data)
        }
        catch (e) {
            try {
                console.log(e);
                setMessage(e.response.data.Message)
            }
            catch {
                console.log(e);
                setMessage("API is not connected")
            }
        }
    }
    async function fetchTripData() {
        // Fetching Trip Data
        try {
            const response = await axios.get(
                process.env.NEXT_PUBLIC_api_root + '/api/customer/trip/' + tripId,
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                }
            )
            setData(response.data)
            setBookedSeat(response.data.bookedSeat)
            console.log(data)
            // console.log(response.data)
        }
        catch (e) {
            try {
                console.log(e);
                setMessage(e.response.data.Message)
            }
            catch {
                console.log(e);
                setMessage("API is not connected")
            }
        }
    }
    useEffect(() => {
        if (id !== undefined) {
            fetchTicketData();
            if (tripId !== undefined) {
                fetchTripData();
            }
        }
        // console.log(router.query.id)
    }, [id, tripId])
    return (
        <>
            <CustomerHeader title="Bus Ticketing System" pagename="Employee: Manage Notice" />
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                    <div className="mx-auto w-1/2">
                        {
                            data == null
                                ? "Data lis loading"
                                :
                                <ul className="grid gap-6 md:grid-cols-2">
                                    <ul className="my-auto">
                                        Bus ID: {data?.bus_id}<br />
                                        Bus From: {data?.depot?.name}<br />
                                        Bus To: {data?.destination?.name}<br />
                                        Bus Start at: {data?.startTime}<br />
                                        Bus End at: {data?.endTime}<br />

                                        Trip ID: {ticketData?.id}<br />
                                        Price: {ticketData?.ammount}<br />
                                        Status: {ticketData?.status}<br />
                                        Cupon: {ticketData?.cupon}<br />
                                    </ul>
                                    <ul className="grid gap-6 md:grid-cols-4">
                                        {numbers.map((i) =>
                                            <li key={i}>
                                                {
                                                    ticketSeat.includes(i)
                                                        ? <input type="checkbox" checked={true} readOnly id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" />
                                                        : bookedSeat.includes(i)
                                                            ? <input type="checkbox" disabled id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" />
                                                            : <input type="checkbox" checked={false} readOnly id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" />
                                                    // <input type="checkbox" id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" checked={true} readOnly
                                                    //     {
                                                    //     ...ticketSeat.includes(i)
                                                    //         ? "checked={true} readOnly"
                                                    //         : bookedSeat.includes(i)
                                                    //             ? "disabled"
                                                    //             : "checked={false} readOnly"
                                                    //     }
                                                    // />
                                                }
                                                <label htmlFor={"seat" + i} className="inline-flex items-center justify-center w-full p-5 text-gray-500 bg-white peer-checked:bg-blue-400 peer-disabled:bg-red-300 border-2 border-gray-200 rounded-lg cursor-pointer dark:hover:text-gray-300 dark:border-gray-700 peer-checked:border-blue-600 hover:text-gray-600 dark:peer-checked:text-gray-300 peer-checked:text-gray-600 hover:bg-gray-50 dark:text-gray-400 dark:bg-gray-800 dark:hover:bg-gray-700">
                                                    <div className="block">
                                                        <div className="w-full text-lg font-semibold">{i}</div>
                                                    </div>
                                                </label>
                                            </li>
                                        )}
                                    </ul>
                                </ul>
                        }
                    </div>

                </div>
            </div>
            <CustomerFooter />
        </>
    )
}