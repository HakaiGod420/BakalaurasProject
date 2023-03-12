import { DatePicker } from "antd";
import { RangePickerProps } from "antd/es/date-picker";
import dayjs from "dayjs";
import customParseFormat from "dayjs/plugin/customParseFormat";
import { useState } from "react";
import { useWizard } from "react-use-wizard";

dayjs.extend(customParseFormat);

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

function InviteStep4({ stepNumber, setStepNumber }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [rules, setRules] = useState<string>("");

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const range = (start: number, end: number) => {
    const result = [];
    for (let i = start; i < end; i++) {
      result.push(i);
    }
    return result;
  };

  const disabledDate: RangePickerProps["disabledDate"] = (current) => {
    // Can not select days before today and today
    return current && current < dayjs().endOf("day");
  };

  const disabledDateTime = () => ({
    disabledHours: () => range(0, 24).splice(4, 20),
    disabledMinutes: () => range(30, 60),
    disabledSeconds: () => [55, 56],
  });

  const inputHandlerSkip = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Finish your invatation
        </h1>
        <p className="text-center p-2">
          Write the rules of the tabletop game you are creating now. You can
          skip this step, but others may not know how to play your game.
        </p>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Meet date</p>
          </div>
          <div className="p-2 flex justify-center">
            <DatePicker
              style={{ backgroundColor: "white", color: "red" }}
              format="YYYY-MM-DD HH:mm:ss"
              disabledDate={disabledDate}
              disabledTime={disabledDateTime}
              showTime={{ defaultValue: dayjs("00:00:00", "HH:mm:ss") }}
              dateRender={(current) => {
                return (
                  <div className="ant-picker-cell-inner" style={{}}>
                    {current.date()}
                  </div>
                );
              }}
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Finish
        </button>
      </div>
    </div>
  );
}

export default InviteStep4;
